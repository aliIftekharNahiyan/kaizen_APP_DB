
using Abp.Application.Services;
using Kaizen.Entities.AcademicYears.Dto;

namespace Kaizen.Entities.AcademicYears
{
    public interface IAcademicYearAppService : IAsyncCrudAppService<AcademicYearDto, long, PagedAcademicYearRequestDto, CreateAcademicYearDto, AcademicYearDto>
    {

    }
}