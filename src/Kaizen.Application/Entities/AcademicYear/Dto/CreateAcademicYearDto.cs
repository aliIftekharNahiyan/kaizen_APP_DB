
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Kaizen.Entities.AcademicYears.Dto
{
    [AutoMapTo(typeof(AcademicYear))]
    public class CreateAcademicYearDto : EntityDto<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool Archived { get; set; } = false;
    }
}