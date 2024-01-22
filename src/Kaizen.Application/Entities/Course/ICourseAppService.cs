
using Abp.Application.Services;
using Kaizen.Entities.AcademicTerms;
using Kaizen.Entities.AcademicTerms.Dto;
using Kaizen.Entities.AcademicYears.Dto;
using Kaizen.Entities.Courses.Dto;

namespace Kaizen.Entities.Courses
{
    public interface ICourseAppService : IAsyncCrudAppService<CourseDto, long, PagedCourseRequestDto, CreateCourseDto, CourseDto>
    {

    }
}