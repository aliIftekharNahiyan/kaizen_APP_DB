
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.Courses;

namespace Kaizen.Entities.Courses.Dto
{
    [AutoMapTo(typeof(Course))]
    public class CreateCourseDto : EntityDto<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [Required]
        public long UniversityId { get; set; }
    }
}