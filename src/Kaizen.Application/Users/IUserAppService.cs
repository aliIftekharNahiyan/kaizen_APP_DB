using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Kaizen.Entities.Skills.Dto;
using Kaizen.Roles.Dto;
using Kaizen.Users.Dto;

namespace Kaizen.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task DeActivate(EntityDto<long> user);
        Task Activate(EntityDto<long> user);
        Task<ListResultDto<RoleDto>> GetRoles();
        Task<ListResultDto<SkillDto>> GetSkills();
        Task ChangeLanguage(ChangeUserLanguageDto input);
        Task UpdateRolesAsync(long userId, string[] roleNames);

        Task<bool> ChangePassword(ChangePasswordDto input);
    }
}