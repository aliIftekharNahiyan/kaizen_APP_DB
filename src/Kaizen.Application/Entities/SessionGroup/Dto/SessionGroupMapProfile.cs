
            using AutoMapper;
            using Kaizen.Entities.SessionGroups;
            using Kaizen.Entities.SessionGroups.Dto;

            namespace Kaizen.Users.Dto
            {
                public class SessionGroupMapProfile : Profile
                {
                    public SessionGroupMapProfile()
                    {
                        CreateMap<SessionGroup, SessionGroupDto>();
                        CreateMap<SessionGroup, CreateSessionGroupDto>();

                        CreateMap<CreateSessionGroupDto, SessionGroup>();
                    }
                }
            }