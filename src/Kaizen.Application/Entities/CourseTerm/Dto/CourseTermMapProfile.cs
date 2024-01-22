
using AutoMapper;
using Kaizen.Entities.Courses;
using Kaizen.Entities.Courses.Dto;
using Kaizen.Entities.CourseTerms;
using Kaizen.Entities.CourseTerms.Dto;


namespace Kaizen.CourseTerms.Dto
{
    public class CourseTermMapProfile : Profile
    {
        public CourseTermMapProfile()
        {
            CreateMap<CourseTerm, CourseTermDto>();
            CreateMap<CourseTerm, CreateCourseTermDto>();

            CreateMap<CreateCourseTermDto, CourseTerm>();
        }
    }
}