
using Abp.Application.Services;
using Kaizen.Entities.ChecklistItems.Dto;

namespace Kaizen.Entities.ChecklistItems
{
    public interface IChecklistItemAppService : IAsyncCrudAppService<ChecklistItemDto, long, PagedChecklistItemResultRequestDto, CreateChecklistItemDto, ChecklistItemDto>
    {

    }
}