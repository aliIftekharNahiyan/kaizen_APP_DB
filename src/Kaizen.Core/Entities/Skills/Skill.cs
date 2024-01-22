using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace Kaizen.Entities.Skills
{
    public class Skill:Entity<long>
    {

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public int SkillLevel { get; set; }

        public decimal? Rate { get; set; } 

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }=false;

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}