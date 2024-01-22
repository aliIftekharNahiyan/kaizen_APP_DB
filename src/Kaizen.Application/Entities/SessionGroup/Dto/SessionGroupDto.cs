
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.FundingBodys.Dto;
using Kaizen.Entities.SupportTypes.Dto;

namespace Kaizen.Entities.SessionGroups.Dto
{
    [AutoMapTo(typeof(SessionGroup))]
    public class SessionGroupDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public long SupportTypeId { get; set; }

        public long FundingBodyId { get; set; }

        public long UserId { get; set; }

        public DateTime CreationDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ExpiryDate { get; set; }

        public int AllocatedHours { get; set; }

        public int AllocatedBudget { get; set; }

        public bool Deleted { get; set; } = false;


        public virtual SupportTypeDto SupportType { get; set; }

        public virtual FundingBodyDto FundingBody { get; set; }
    }
}