using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;
using Kaizen.Entities.LinkTables;

namespace Kaizen.Entities.Checklists
{
    [Audited]
    public class Checklist: Entity<long>
    {
        [Required]
        [StringLength(512)]
        public string Name { get; set; }

        [Required]
        public bool Deleted { get; set; } = false;

        public virtual List<ChecklistCheckListItem> ChecklistItems { get; set; }
    }
}
