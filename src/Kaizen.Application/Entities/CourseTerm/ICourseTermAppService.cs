
using Abp.Application.Services;
using Kaizen.Entities.AcademicTerms;
using Kaizen.Entities.AcademicTerms.Dto;
using Kaizen.Entities.AcademicYears.Dto;
using Kaizen.Entities.Courses.Dto;
using Kaizen.Entities.CourseTerms.Dto;

namespace Kaizen.Entities.CourseTerms
{
    public interface ICourseTermAppService : IAsyncCrudAppService<CourseTermDto, long, PagedCourseTermRequestDto, CreateCourseTermDto, CourseTermDto>
    {

    }
}