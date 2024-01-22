
using Abp.Application.Services;
using Kaizen.Entities.Communications;
using Kaizen.Entities.Communications.Dto;
using Kaizen.Entities.AcademicYears.Dto;

namespace Kaizen.Entities.Communications
{
    public interface ICommunicationAppService : IAsyncCrudAppService<CommunicationDto, long, PagedCommunicationRequestDto, CreateCommunicationDto, CommunicationDto>
    {

    }
}