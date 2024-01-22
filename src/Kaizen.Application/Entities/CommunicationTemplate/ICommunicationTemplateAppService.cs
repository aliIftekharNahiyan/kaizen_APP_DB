
using Abp.Application.Services;
using Kaizen.Entities.CommunicationTemplates;
using Kaizen.Entities.CommunicationTemplates.Dto;
using Kaizen.Entities.AcademicYears.Dto;

namespace Kaizen.Entities.CommunicationTemplates
{
    public interface ICommunicationTemplateAppService : IAsyncCrudAppService<CommunicationTemplateDto, long, PagedCommunicationTemplateRequestDto, CreateCommunicationTemplateDto, CommunicationTemplateDto>
    {

    }
}