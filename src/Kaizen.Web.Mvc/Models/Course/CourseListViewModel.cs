
using System.Collections.Generic;
using Kaizen.Entities.Courses.Dto;

namespace Kaizen.Web.Models.Course
{
    public class CourseListViewModel
    {
        public IReadOnlyList<CourseDto> Entities { get; set; }
    }
}