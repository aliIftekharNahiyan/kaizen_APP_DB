
using Abp.Application.Services;
using Kaizen.Entities.AcademicTerms;
using Kaizen.Entities.AcademicTerms.Dto;
using Kaizen.Entities.AcademicYears.Dto;
using Kaizen.Entities.InfoCheckLists.Dto;

namespace Kaizen.Entities.InfoCheckLists
{
    public interface IInfoCheckListAppService : IAsyncCrudAppService<InfoCheckListDto, long, PagedInfoCheckListRequestDto, CreateInfoCheckListDto, InfoCheckListDto>
    {

    }
}