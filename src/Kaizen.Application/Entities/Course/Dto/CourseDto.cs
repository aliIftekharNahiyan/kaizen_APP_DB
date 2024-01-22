
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

namespace Kaizen.Entities.Courses.Dto
{
    [AutoMapTo(typeof(Course))]
    public class CourseDto : EntityDto<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [Required]
        public long UniversityId { get; set; }
        public virtual University University { get; set; }
    }
}