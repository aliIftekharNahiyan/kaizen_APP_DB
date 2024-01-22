
using Abp.Application.Services;
using Kaizen.Entities.ChecklistItems.Dto;
using Kaizen.Entities.Checklists.Dto;
using System.Collections.Generic;

namespace Kaizen.Entities.Checklists
{
    public interface IChecklistAppService : IAsyncCrudAppService<ChecklistDto, long, PagedChecklistResultRequestDto, CreateChecklistDto, ChecklistDto>
    {
        List<ChecklistCheckListItemDto> GetItemsForChecklist(ChecklistDto input);
    }
}