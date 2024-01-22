using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;
using Kaizen.Entities.FundingBodys;
using Kaizen.Entities.SupportTypes;

namespace Kaizen.Entities.SessionGroups
{
    [Audited]
    public class SessionGroup : Entity<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public long SupportTypeId { get; set; }

        [Required]
        public long FundingBodyId { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public int AllocatedHours { get; set; }

        public int AllocatedBudget { get; set; }

        [Required]
        public bool Deleted { get; set; } = false;

        [ForeignKey(nameof(SupportTypeId))]
        public virtual SupportType SupportType { get; set; }

        [ForeignKey(nameof(FundingBodyId))]
        public virtual FundingBody FundingBody { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

    }
}
