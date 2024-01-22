
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;

namespace Kaizen.Entities.SupportSessions.Dto
{
    [AutoMapTo(typeof(SupportSession))]
    public class CreateSupportSessionDto : EntityDto<long>
    {
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

        public int Status { get; set; }
        [Required]
        public long UserId { get; set; }

    }
}