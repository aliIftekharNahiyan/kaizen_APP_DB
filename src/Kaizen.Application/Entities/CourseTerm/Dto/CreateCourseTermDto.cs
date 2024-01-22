
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Kaizen.Entities.CourseTerms.Dto
{
    [AutoMapTo(typeof(CourseTerm))]
    public class CreateCourseTermDto : EntityDto<long>
    {
        [Required]
        public int LengthOfTerm { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        [Required]
        public long CourseId { get; set; }
    }
}