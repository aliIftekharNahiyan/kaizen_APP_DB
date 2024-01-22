using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;
using Kaizen.Entities.FundingBodys;
using Kaizen.Entities.SessionGroups;
using Kaizen.Entities.SessionTypes;
using Kaizen.Entities.SupportTypes;
using Kaizen.Enums;

namespace Kaizen.Entities.SupportSessions
{
    [Audited]
    public class SupportSession : Entity<long>
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public long SessionGroupId { get; set; }

        [Required]
        public long SupportTypeId { get; set; }

        [Required]
        public long FundingBodyId { get; set; }

        [Required]
        public DateTime SessionStartDate { get; set; }

        [Required]
        public DateTime SessionEndDate { get; set; }

        [Required]
        public long SessionTypeId { get; set; }
        [Required]
        public long SupportProfessionalUserId { get; set; }

        public SupportSessionStatus Status { get; set; }

        [ForeignKey(nameof(SessionGroupId))]
        public virtual SessionGroup SessionGroup { get; set; }

        [ForeignKey(nameof(SupportTypeId))]
        public virtual SupportType SupportType { get; set; }

        [ForeignKey(nameof(FundingBodyId))]
        public virtual FundingBody FundingBody { get; set; }
        [ForeignKey(nameof(SessionTypeId))]
        public virtual SessionType SessionType { get; set; }

        [ForeignKey(nameof(SupportProfessionalUserId))]
        public virtual User SupportProfessionalUser { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

    }
}
