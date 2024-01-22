
using AutoMapper;
using Kaizen.Entities.SessionTypes;
using Kaizen.Entities.SessionTypes.Dto;

namespace Kaizen.Users.Dto
{
    public class SessionTypeDtoMapProfile : Profile
    {
        public SessionTypeDtoMapProfile()
        {
            CreateMap<SessionType, SessionTypeDto>();
            CreateMap<SessionType, CreateSessionTypeDto>();

            CreateMap<CreateSessionTypeDto, SessionType>();
        }
    }
}