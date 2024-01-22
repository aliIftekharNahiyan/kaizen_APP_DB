
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.Courses.Dto
{
    public class PagedCourseRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
        public long? UniversityId { get; set; }
    }
}