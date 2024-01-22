
using AutoMapper;
using Kaizen.Entities.SupportSessions;
using Kaizen.Entities.SupportSessions.Dto;

namespace Kaizen.Users.Dto
{
    public class SupportSessionMapProfile : Profile
    {
        public SupportSessionMapProfile()
        {
            CreateMap<SupportSession, SupportSessionDto>();
            CreateMap<SupportSession, SupportSessionCalendarDto>();
            CreateMap<SupportSession, CreateSupportSessionDto>();

            CreateMap<CreateSupportSessionDto, SupportSession>();
        }
    }
}