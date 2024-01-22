using System.ComponentModel.DataAnnotations;
using System;
using Abp.AutoMapper;
using Kaizen.Entities.SessionGroups.Dto;
using Kaizen.Entities.SessionGroups;
using AutoMapper.Configuration.Annotations;

namespace Kaizen.Web.Models.GenericBulkProcess
{   
    public class SessionGroupViewModel
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
        public DateTime ExpiryDate { get; set; }

        public int AllocatedHours { get; set; }

        public int AllocatedBudget { get; set; }

        public bool Deleted { get; set; } = false;

        [Required]
        public string UserId { get; set; }
    }
}