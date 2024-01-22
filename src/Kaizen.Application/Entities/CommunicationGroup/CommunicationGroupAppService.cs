
using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.FundingBodys.Dto;
using Abp.Linq.Extensions;
using Kaizen.Entities.CommunicationGroups.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Kaizen.MultiTenancy;
using Abp.UI;
using Kaizen.Entities.SessionGroups.Dto;
using System.Collections.Generic;
using Kaizen.Entities.LinkTables;

namespace Kaizen.Entities.CommunicationGroups
{
    public class CommunicationGroupAppService : AsyncCrudAppService<CommunicationGroup, CommunicationGroupDto, long, PagedCommunicationGroupRequestDto, CreateCommunicationGroupDto, CommunicationGroupDto>, ICommunicationGroupAppService
    {
        public CommunicationGroupAppService(
            IRepository<CommunicationGroup, long> repository)
            : base(repository)
        {
        }

        public override async Task<CommunicationGroupDto> GetAsync(EntityDto<long> input)
        {
            CommunicationGroup CommunicationGroup = await Repository.GetAllIncluding().SingleOrDefaultAsync(a => a.Id == input.Id);

            return ObjectMapper.Map<CommunicationGroupDto>(CommunicationGroup);
        }


        public override async Task<PagedResultDto<CommunicationGroupDto>> GetAllAsync(PagedCommunicationGroupRequestDto input)
        {
            var query = Repository.GetAllIncluding();

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            return new PagedResultDto<CommunicationGroupDto> { Items = ObjectMapper.Map<List<CommunicationGroupDto>>(query), TotalCount = totalCount };
        }

        public override async Task<CommunicationGroupDto> CreateAsync(CreateCommunicationGroupDto input)
        {
            CheckCreatePermission();

            CommunicationGroup entity = MapToEntity(input);

            entity.CommunicationGroupUsers = input.UserList.Select(a => new CommunicationGroupUser
            {
                UserId = a
            }).ToList();

            await Repository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public override async Task<CommunicationGroupDto> UpdateAsync(CommunicationGroupDto input)
        {
            CheckUpdatePermission();

            var entity = Repository.GetAllIncluding(a => a.CommunicationGroupUsers).SingleOrDefault(a => a.Id == input.Id);

            MapToEntity(input, entity);

            entity.CommunicationGroupUsers = input.UserList.Select(a => new CommunicationGroupUser
            {
                UserId = a
            }).ToList();

            await Repository.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            CheckDeletePermission();

            var entity = await GetEntityByIdAsync(input.Id);

            entity.Deleted = true;

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        protected override IQueryable<CommunicationGroup> ApplySorting(IQueryable<CommunicationGroup> query, PagedCommunicationGroupRequestDto input)
        {
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                return query.OrderBy(input.Sorting);
            }
            else
            {
                return query.OrderBy("name asc");
            }
        }

        protected override IQueryable<CommunicationGroup> CreateFilteredQuery(PagedCommunicationGroupRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding();
        }
    }
}