
using Abp.Application.Services;
using Kaizen.Entities.CommunicationGroups;
using Kaizen.Entities.CommunicationGroups.Dto;
using Kaizen.Entities.AcademicYears.Dto;

namespace Kaizen.Entities.CommunicationGroups
{
    public interface ICommunicationGroupAppService : IAsyncCrudAppService<CommunicationGroupDto, long, PagedCommunicationGroupRequestDto, CreateCommunicationGroupDto, CommunicationGroupDto>
    {

    }
}