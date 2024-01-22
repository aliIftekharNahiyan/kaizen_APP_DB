
using Abp.Application.Services;
using Kaizen.Entities.Universitys.Dto;

namespace Kaizen.Entities.Universitys
{
    public interface IUniversityAppService : IAsyncCrudAppService<UniversityDto, long, PagedUniversityResultRequestDto, CreateUniversityDto, UniversityDto>
    {

    }
}