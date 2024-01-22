using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Entities.Checklists;

namespace Kaizen.Entities.LinkTables
{
    [Audited]
    public class ChecklistCheckListItem: Entity<long>
    {
        public long CheckListId { get; set; }

        public long ChecklistItemId { get; set; }

        [ForeignKey(nameof(CheckListId))]
        public virtual Checklist Checklist { get; set; }

        [ForeignKey(nameof(ChecklistItemId))]
        public virtual ChecklistItem ChecklistItem { get; set; }
    }
}
