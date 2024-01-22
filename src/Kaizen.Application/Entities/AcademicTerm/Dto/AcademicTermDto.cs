
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.AcademicYears;
using Kaizen.Entities.AcademicYears.Dto;
using Kaizen.Entities.Companies.Dto;
using Kaizen.Entities.SupportTypes.Dto;

namespace Kaizen.Entities.AcademicTerms.Dto
{
    [AutoMapTo(typeof(AcademicTerm))]
    public class AcademicTermDto : EntityDto<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        [Required]
        public bool Archived { get; set; } = false;
        public long AcademicYearId { get; set; }

        public virtual AcademicYearDto AcademicYear { get; set; }
    }
}