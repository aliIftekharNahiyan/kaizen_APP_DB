
using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.Checklists.Dto;
using System.Threading.Tasks;
using Kaizen.Entities.ChecklistItems.Dto;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Entities.Checklists
{
    public class ChecklistAppService : AsyncCrudAppService<Checklist, ChecklistDto, long, PagedChecklistResultRequestDto, CreateChecklistDto, ChecklistDto>, IChecklistAppService
    {
        public ChecklistAppService(
            IRepository<Checklist, long> repository)
            : base(repository)
        {
        }

        public List<ChecklistCheckListItemDto> GetItemsForChecklist(ChecklistDto input)
        {
            return ObjectMapper.Map<List<ChecklistCheckListItemDto>>(Repository.GetAllIncluding().Include(a => a.ChecklistItems).ThenInclude(a => a.ChecklistItem).SingleOrDefault(a => a.Id == input.Id).ChecklistItems);
        }

        public override async Task<ChecklistDto> UpdateAsync(ChecklistDto input)
        {
            CheckUpdatePermission();

            var entity = Repository.GetAllIncluding(a => a.ChecklistItems).SingleOrDefault(a => a.Id == input.Id);

            MapToEntity(input, entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        protected override IQueryable<Checklist> ApplySorting(IQueryable<Checklist> query, PagedChecklistResultRequestDto input)
        {
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                return query.OrderBy(input.Sorting);
            }

            return base.ApplySorting(query, input);
        }

        protected override IQueryable<Checklist> CreateFilteredQuery(PagedChecklistResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding();
        }
    }
}