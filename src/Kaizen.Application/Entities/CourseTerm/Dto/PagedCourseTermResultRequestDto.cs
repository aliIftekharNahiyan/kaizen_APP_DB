
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.CourseTerms.Dto
{
    public class PagedCourseTermRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
        public long? CourseId { get; set; }
    }
}