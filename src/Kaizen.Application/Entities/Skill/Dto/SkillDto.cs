using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Kaizen.Entities.Skills.Dto
{
    [AutoMapFrom(typeof(Skill))]
    public class SkillDto : EntityDto<long>
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public int SkillLevel { get; set; }

        public decimal? Rate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; } = false;

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}