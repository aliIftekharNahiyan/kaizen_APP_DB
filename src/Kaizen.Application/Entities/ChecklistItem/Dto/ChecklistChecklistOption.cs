using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.Checklists.Dto;
using Kaizen.Entities.LinkTables;

namespace Kaizen.Entities.ChecklistItems.Dto
{
    [AutoMapTo(typeof(ChecklistCheckListItem))]
    public class ChecklistCheckListItemDto : EntityDto<long>
    {
        public long CheckListId { get; set; }

        public long ChecklistItemId { get; set; }

        [ForeignKey(nameof(CheckListId))]
        public virtual ChecklistDto Checklist { get; set; }

        [ForeignKey(nameof(ChecklistItemId))]
        public virtual ChecklistItemDto ChecklistItem { get; set; }
    }
}