
using AutoMapper;
using Kaizen.Entities.Communications;
using Kaizen.Entities.Communications.Dto;
using Kaizen.Entities.AcademicYears.Dto;


namespace Kaizen.Communications.Dto
{
    public class CommunicationMapProfile : Profile
    {
        public CommunicationMapProfile()
        {
            CreateMap<Communication, CommunicationDto>();
            CreateMap<Communication, CreateCommunicationDto>();

            CreateMap<CreateCommunicationDto, Communication>();
        }
    }
}