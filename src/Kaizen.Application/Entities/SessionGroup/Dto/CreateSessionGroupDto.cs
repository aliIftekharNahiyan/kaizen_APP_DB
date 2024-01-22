
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;

namespace Kaizen.Entities.SessionGroups.Dto
{
    [AutoMapTo(typeof(SessionGroup))]
    public class CreateSessionGroupDto : EntityDto<long>
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
    }
}