
using Abp.Application.Services;
using Kaizen.Entities.AcademicTerms;
using Kaizen.Entities.AcademicTerms.Dto;
using Kaizen.Entities.AcademicYears.Dto;

namespace Kaizen.Entities.AcademicTerms
{
    public interface IAcademicTermAppService : IAsyncCrudAppService<AcademicTermDto, long, PagedAcademicTermRequestDto, CreateAcademicTermDto, AcademicTermDto>
    {

    }
}