using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;

namespace Kaizen.Entities.Checklists
{
    [Audited]
    public class ChecklistItem : Entity<long>
    {
        [Required]
        [StringLength(1024)]
        public string Name { get; set; }

        [Required]
        public bool Deleted { get; set; } = false;

        public virtual List<ChecklistItemOption> Options { get; set; }
    }
}
