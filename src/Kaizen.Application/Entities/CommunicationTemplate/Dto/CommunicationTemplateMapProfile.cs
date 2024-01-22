
using AutoMapper;
using Kaizen.Entities.CommunicationTemplates;
using Kaizen.Entities.CommunicationTemplates.Dto;
using Kaizen.Entities.AcademicYears.Dto;
using Kaizen.Entities.LinkTables;

namespace Kaizen.CommunicationTemplates.Dto
{
    public class CommunicationTemplateMapProfile : Profile
    {
        public CommunicationTemplateMapProfile()
        {
            CreateMap<CommunicationTemplate, CommunicationTemplateDto>();
            CreateMap<CommunicationTemplate, CreateCommunicationTemplateDto>();

            CreateMap<CreateCommunicationTemplateDto, CommunicationTemplate>();
        }
    }
}