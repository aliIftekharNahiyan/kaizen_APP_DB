
using System.Collections.Generic;
using Kaizen.Entities.Courses.Dto;
using Kaizen.Entities.CourseTerms.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaizen.Web.Models.CourseTerm
{
    public class EditCourseTermModalViewModel
    {
        public CourseTermDto CourseTerm { get; set; }
    }
}