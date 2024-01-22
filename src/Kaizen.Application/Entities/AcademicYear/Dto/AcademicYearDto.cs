
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.Companies.Dto;
using Kaizen.Entities.SupportTypes.Dto;

namespace Kaizen.Entities.AcademicYears.Dto
{
    [AutoMapTo(typeof(AcademicYear))]
    public class AcademicYearDto : EntityDto<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        [Required]
        public bool Archived { get; set; } = false;
        public long? CompanyId { get; set; }

        public virtual CompanyDto Company { get; set; }
    }
}