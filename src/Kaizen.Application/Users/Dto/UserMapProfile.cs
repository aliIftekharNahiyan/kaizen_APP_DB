using AutoMapper;
using Kaizen.Authorization.Users;
using Kaizen.Entities.LinkTables;
using Kaizen.Entities.UserClientSupports;

namespace Kaizen.Users.Dto
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.DpStatus, opt => opt.MapFrom(src => src.DpStatus))
                .ForMember(x => x.AgreeTC, opt => opt.MapFrom(src => src.AgreeTC))
                .ForMember(x => x.Archived, opt => opt.MapFrom(src => src.Archived));
            CreateMap<CreateUserDto, User>();
            CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());
            CreateMap<UserSkillDto, UserSkill>().ReverseMap();
            CreateMap<UserLivingRegionLocationDto, UserLivingRegionLocation>().ReverseMap();
            CreateMap<UserWorkRegionLocationDto, UserWorkRegionLocation>().ReverseMap();
            CreateMap<UserClientSupportDto, UserClientSupport>().ReverseMap();
        }
    }
}
