
using System.Collections.Generic;
using Kaizen.Entities.Courses.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaizen.Web.Models.Course
{
    public class EditCourseModalViewModel
    {
        public CourseDto Course { get; set; }
    }
}