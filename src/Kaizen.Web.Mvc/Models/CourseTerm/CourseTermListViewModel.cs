
using System.Collections.Generic;
using Kaizen.Entities.Courses.Dto;
using Kaizen.Entities.CourseTerms.Dto;

namespace Kaizen.Web.Models.CourseTerm
{
    public class CourseTermListViewModel
    {
        public IReadOnlyList<CourseTermDto> Entities { get; set; }
    }
}