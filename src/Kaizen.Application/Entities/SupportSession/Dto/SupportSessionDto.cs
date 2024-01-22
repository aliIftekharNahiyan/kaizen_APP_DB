
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Authorization.Users;
using Kaizen.Entities.FundingBodys;
using Kaizen.Entities.FundingBodys.Dto;
using Kaizen.Entities.SessionGroups;
using Kaizen.Entities.SessionTypes;
using Kaizen.Entities.SupportTypes;
using Kaizen.Entities.SupportTypes.Dto;
using Kaizen.Enums;

namespace Kaizen.Entities.SupportSessions.Dto
{
    [AutoMapTo(typeof(SupportSession))]
    public class SupportSessionDto : EntityDto<long>
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
        public string StatusText { get { return this.Status.ToString(); } }

        public virtual SessionGroup SessionGroup { get; set; }

        public virtual SupportType SupportType { get; set; }

        public virtual FundingBody FundingBody { get; set; }

        public virtual SessionType SessionType { get; set; }

        public virtual User SupportProfessionalUser { get; set; }

        public virtual User User { get; set; }
    }
}