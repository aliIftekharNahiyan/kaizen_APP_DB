
using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.FundingBodys.Dto;
using Abp.Linq.Extensions;
using Kaizen.Entities.CommunicationTemplates.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Kaizen.MultiTenancy;
using Abp.UI;
using Kaizen.Entities.SessionGroups.Dto;
using System.Collections.Generic;
using Kaizen.Entities.LinkTables;

namespace Kaizen.Entities.CommunicationTemplates
{
    public class CommunicationTemplateAppService : AsyncCrudAppService<CommunicationTemplate, CommunicationTemplateDto, long, PagedCommunicationTemplateRequestDto, CreateCommunicationTemplateDto, CommunicationTemplateDto>, ICommunicationTemplateAppService
    {
        public CommunicationTemplateAppService(
            IRepository<CommunicationTemplate, long> repository)
            : base(repository)
        {
        }

        public override async Task<CommunicationTemplateDto> GetAsync(EntityDto<long> input)
        {
            CommunicationTemplate CommunicationTemplate = await Repository.GetAllIncluding().SingleOrDefaultAsync(a => a.Id == input.Id);

            return ObjectMapper.Map<CommunicationTemplateDto>(CommunicationTemplate);
        }


        public override async Task<PagedResultDto<CommunicationTemplateDto>> GetAllAsync(PagedCommunicationTemplateRequestDto input)
        {
            var query = Repository.GetAllIncluding();

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            return new PagedResultDto<CommunicationTemplateDto> { Items = ObjectMapper.Map<List<CommunicationTemplateDto>>(query), TotalCount = totalCount };
        }

        public override async Task<CommunicationTemplateDto> CreateAsync(CreateCommunicationTemplateDto input)
        {
            CheckCreatePermission();

            CommunicationTemplate entity = MapToEntity(input);

            await Repository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public override async Task<CommunicationTemplateDto> UpdateAsync(CommunicationTemplateDto input)
        {
            CheckUpdatePermission();

            var entity = Repository.GetAllIncluding().SingleOrDefault(a => a.Id == input.Id);

            MapToEntity(input, entity);

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

        protected override IQueryable<CommunicationTemplate> ApplySorting(IQueryable<CommunicationTemplate> query, PagedCommunicationTemplateRequestDto input)
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

        protected override IQueryable<CommunicationTemplate> CreateFilteredQuery(PagedCommunicationTemplateRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding();
        }
    }
}