using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using Kaizen.Authorization;
using Kaizen.Authorization.Roles;
using Kaizen.Authorization.Users;
using Kaizen.Entities.LinkTables;
using Kaizen.Entities.Skills;
using Kaizen.Entities.Skills.Dto;
using Kaizen.Entities.UserClientSupports;
using Kaizen.Entities.UserKins;
using Kaizen.Entities.UserLookups;
using Kaizen.Roles.Dto;
using Kaizen.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserSkill, long> _userSkillRepository;
        private readonly IRepository<UserWorkRegionLocation, long> _userWorkRegionRepository;
        private readonly IRepository<UserLivingRegionLocation, long> _userLivingRegionLocation;
        private readonly IRepository<UserClientSupport, long> _userClientSupportRepository;
        private readonly IRepository<UserLookup, long> _userTypRepository;

        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository, IRepository<UserClientSupport, long> userClientSupportRepository,
            IRepository<UserSkill, long> userSkillRepository, IRepository<UserWorkRegionLocation, long> userWorkRegionRepository,
             IRepository<UserLivingRegionLocation, long> userLivingRegionLocation, IRepository<UserLookup, long> userTypRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
            _userSkillRepository = userSkillRepository;
            _userWorkRegionRepository = userWorkRegionRepository;
            _userLivingRegionLocation = userLivingRegionLocation;
            _userTypRepository = userTypRepository;
            _userClientSupportRepository = userClientSupportRepository;
        }

        public override async Task<UserDto> GetAsync(EntityDto<long> input)
        {
            try
            {

                await _userWorkRegionRepository.GetAllIncluding(x => x.RegionLocation).Where(x => x.UserId == input.Id).Select(x => x.RegionLocation).ToListAsync();
                await _userLivingRegionLocation.GetAllIncluding(x => x.RegionLocation).Where(x => x.UserId == input.Id).Select(x => x.RegionLocation).ToListAsync();
                await _userSkillRepository.GetAllIncluding(x => x.Skill).Where(x => x.UserId == input.Id).Select(x => x.Skill).ToListAsync();              

                User user = await Repository.GetAllIncluding(a => a.University,
                    c => c.CustomerType, x => x.Skills,
                    l => l.LivingRegionLocations, w => w.WorkRegionLocations,
                    p => p.PaymentTerm, v => v.VisibilityPermission, u => u.UserType, p => p.Pronoun,
                    ucs => ucs.UserClientSupports).SingleOrDefaultAsync(a => a.Id == input.Id);
                return ObjectMapper.Map<UserDto>(user);
            }
            catch (Exception e)
            {
                return new UserDto();
            }

        }


        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();


            return MapToEntityDto(user);
        }

        public override async Task<UserDto> UpdateAsync(UserDto input)
        {
            CheckUpdatePermission();
            await _userSkillRepository.DeleteAsync(x => x.UserId == input.Id);            
            await _userWorkRegionRepository.DeleteAsync(x => x.UserId == input.Id);
            await _userLivingRegionLocation.DeleteAsync(x => x.UserId == input.Id);
            await _userClientSupportRepository.DeleteAsync(x => x.UserId == input.Id);

            //await CurrentUnitOfWork.SaveChangesAsync();

            var user = await _userManager.GetUserByIdAsync(input.Id);






            MapToEntity(input, user);


            CheckErrors(await _userManager.UpdateAsync(user));

            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            return await GetAsync(input);
        }

        public async Task UpdateRolesAsync(long userId, string[] roleNames)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(userId);

            if (roleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, roleNames));
            }

        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Activation)]
        public async Task Activate(EntityDto<long> user)
        {
            await Repository.UpdateAsync(user.Id, async (entity) =>
            {
                entity.IsActive = true;
            });
        }

        [AbpAuthorize(PermissionNames.Pages_Users_Activation)]
        public async Task DeActivate(EntityDto<long> user)
        {
            await Repository.UpdateAsync(user.Id, async (entity) =>
            {
                entity.IsActive = false;
            });
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }
        public async Task<ListResultDto<SkillDto>> GetSkills()
        {
            //var skill = await _skillRepository.GetAllListAsync();
            //return new ListResultDto<SkillDto>(ObjectMapper.Map<List<SkillDto>>(skill));
            throw new NotImplementedException();
        }
        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roleIds = user.Roles.Select(x => x.RoleId).ToArray();

            var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);

            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();

            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            Role role = null;

            if (!string.IsNullOrEmpty(input.Role))
            {
                role = _roleManager.GetRoleByName(input.Role);
            }

            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.ToLower().Contains(keywordLower) || x.Name.ToLower().Contains(keywordLower) || x.EmailAddress.ToLower().Contains(keywordLower))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive)
                .WhereIf(role != null, x => x.Roles.Any(b => b.RoleId == role.Id));
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            if (await _userManager.CheckPasswordAsync(user, input.CurrentPassword))
            {
                CheckErrors(await _userManager.ChangePasswordAsync(user, input.NewPassword));
            }
            else
            {
                CheckErrors(IdentityResult.Failed(new IdentityError
                {
                    Description = "Incorrect password."
                }));
            }

            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attempting to reset password.");
            }

            var currentUser = await _userManager.GetUserByIdAsync(_abpSession.GetUserId());
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }

            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }

            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return true;
        }

    }
}

