
using AutoMapper;
using Kaizen.Entities.Courses;
using Kaizen.Entities.Courses.Dto;


namespace Kaizen.Entities.Courses.Dto
{
    public class CourseMapProfile : Profile
    {
        public CourseMapProfile()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<Course, CreateCourseDto>();

            CreateMap<CreateCourseDto, Course>();
        }
    }
}