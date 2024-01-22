
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.AcademicYears;
using Kaizen.Entities.Courses.Dto;
using Kaizen.Entities.Companies.Dto;
using Kaizen.Entities.SupportTypes.Dto;
using Kaizen.Entities.Universitys;
using System.ComponentModel.DataAnnotations.Schema;
using Kaizen.Entities.Courses;

namespace Kaizen.Entities.CourseTerms.Dto
{
    [AutoMapTo(typeof(CourseTerm))]
    public class CourseTermDto : EntityDto<long>
    {
        [Required]
        public int LengthOfTerm { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]

        public DateTime? EndDate { get; set; }
        [Required]
        public long CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}