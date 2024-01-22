using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;

namespace Kaizen.Entities.Checklists
{
    [Audited]
    public class ChecklistItemOption : Entity<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public long ChecklistItemId { get; set; }

        [Required]
        public bool Deleted { get; set; } = false;

        [ForeignKey(nameof(ChecklistItemId))]
        public virtual ChecklistItem ChecklistItem { get; set; }
    }
}
