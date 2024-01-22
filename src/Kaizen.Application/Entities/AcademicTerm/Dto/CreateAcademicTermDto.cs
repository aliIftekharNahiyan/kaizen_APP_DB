
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.AcademicTerms;

namespace Kaizen.Entities.AcademicTerms.Dto
{
    [AutoMapTo(typeof(AcademicTerm))]
    public class CreateAcademicTermDto : EntityDto<long>
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
        [Required]
        public long AcademicYearId { get; set; }
    }
}