using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;
using Kaizen.Entities.Checklists;

namespace Kaizen.Entities.SupportTypes
{
    [Audited]
    public class SupportType : Entity<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public long? CheckListId { get; set; }

        public string Description { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public int Margin { get; set; }

        [Required]
        public bool Deleted { get; set; } = false;

        public virtual double SellPrice => Math.Round(((Cost / 100) * Margin) + Cost, 2);

        [ForeignKey(nameof(CheckListId))]
        public virtual Checklist Checklist { get; set; }

    }
}
