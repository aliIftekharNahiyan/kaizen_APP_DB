
using AutoMapper;
using Kaizen.Entities.CommunicationGroups;
using Kaizen.Entities.CommunicationGroups.Dto;
using Kaizen.Entities.AcademicYears.Dto;
using Kaizen.Entities.LinkTables;

namespace Kaizen.CommunicationGroups.Dto
{
    public class CommunicationGroupMapProfile : Profile
    {
        public CommunicationGroupMapProfile()
        {
            CreateMap<CommunicationGroup, CommunicationGroupDto>();
            CreateMap<CommunicationGroup, CreateCommunicationGroupDto>();

            CreateMap<CreateCommunicationGroupDto, CommunicationGroup>();

            CreateMap<CommunicationGroupUserDto, CommunicationGroupUser>();
            CreateMap<CommunicationGroupUser, CommunicationGroupUserDto>();
        }
    }
}