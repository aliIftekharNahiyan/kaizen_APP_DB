using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;using Abp.Application.Services.Dto;
using System.Collections.Generic;
using Kaizen.Entities.UserKins.Dto;
using Kaizen.Entities.UserKinAppService;

using Microsoft.EntityFrameworkCore;

namespace Kaizen.Entities.UserKins
{
    public class UserKinAppService : AsyncCrudAppService<UserKin, UserKinDto, long, PagedUserKinResultRequestDto, CreateUserKinDto, UserKinDto>, IUserKinAppService
    {
        public UserKinAppService(
            IRepository<UserKin, long> repository)
            : base(repository)
        {
        }

     
        public override async Task<UserKinDto> CreateAsync(CreateUserKinDto input)
        {
            input.CreatedBy = AbpSession.UserId.Value;
            input.UserId = input.UserId;
            input.CreatedDate = DateTime.UtcNow;
            input.IsActive = true;
           

            return await base.CreateAsync(input);
        }
        public override async Task<UserKinDto> UpdateAsync(UserKinDto input)
        {
            UserKin userKin = await Repository.GetAsync(input.Id);
            input.CreatedBy=userKin.CreatedBy;
            input.CreatedDate=userKin.CreatedDate;
            input.IsDeleted=userKin.IsDeleted;
            input.IsActive = userKin.IsActive;
            input.UpdatedBy = AbpSession.UserId.Value;
            input.UserId = input.UserId;
            input.UpdatedDate = DateTime.UtcNow;
            


            return await base.UpdateAsync(input);
        }
        public async Task<PagedResultDto<IGrouping<string, UserKinDto>>> GetGroupedAsync(PagedUserKinResultRequestDto input)
        {
            try
            {
                var query = Repository.GetAllIncluding();

                var totalCount = await AsyncQueryableExecuter.CountAsync(query);

                query = ApplySorting(query, input);
                query = ApplyPaging(query, input);

                var notes = ObjectMapper.Map<List<UserKinDto>>(query.ToList().Select(a => new UserKinDto
                {
                    
                   
                   
                    CreatedDate = a.CreatedDate
                }).ToList());


              
                return new PagedResultDto<IGrouping<string, UserKinDto>> { };
            }
            catch(Exception ex)
            {
                
            }

            return new PagedResultDto<IGrouping<string, UserKinDto>> { TotalCount = 0 };
        }


        public override async Task<UserKinDto> GetAsync(EntityDto<long> input)
        {
            UserKin userKin = await Repository.GetAllIncluding(a => a.Relation,c=>c.ContactType).SingleOrDefaultAsync(a => a.Id == input.Id);

            return ObjectMapper.Map<UserKinDto>(userKin);
        }


        protected override IQueryable<UserKin> ApplySorting(IQueryable<UserKin> query, PagedUserKinResultRequestDto input)
        {
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                return query.OrderBy(input.Sorting);
            }
            else
            {
                return query.OrderBy("CreatedBy asc");
            }
        }

        protected override IQueryable<UserKin> CreateFilteredQuery(PagedUserKinResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding(x => x.Relation, c => c.User, b => b.ContactType).Where(x => x.IsActive == true);

        }
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            CheckDeletePermission();
            await Repository.UpdateAsync(input.Id, async (entity) =>
            {
                entity.IsActive = false;
                entity.IsDeleted = true;
            });
           
        }
    }
}