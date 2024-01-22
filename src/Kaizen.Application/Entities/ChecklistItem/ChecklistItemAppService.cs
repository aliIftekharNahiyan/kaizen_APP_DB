
using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.ChecklistItems.Dto;
using Kaizen.Entities.Checklists;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.ChecklistItems
{
    public class ChecklistItemAppService : AsyncCrudAppService<ChecklistItem, ChecklistItemDto, long, PagedChecklistItemResultRequestDto, CreateChecklistItemDto, ChecklistItemDto>, IChecklistItemAppService
    {
        public ChecklistItemAppService(
            IRepository<ChecklistItem, long> repository)
            : base(repository)
        {
        }


        public override async Task<ChecklistItemDto> UpdateAsync(ChecklistItemDto input)
        {
            CheckUpdatePermission();

            var entity = Repository.GetAllIncluding(a => a.Options).SingleOrDefault(a => a.Id == input.Id);

            MapToEntity(input, entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        protected override IQueryable<ChecklistItem> ApplySorting(IQueryable<ChecklistItem> query, PagedChecklistItemResultRequestDto input)
        {
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                return query.OrderBy(input.Sorting);
            }

            return base.ApplySorting(query, input);
        }

        protected override IQueryable<ChecklistItem> CreateFilteredQuery(PagedChecklistItemResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding();
        }
    }
}