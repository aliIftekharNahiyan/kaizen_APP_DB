using AutoMapper;
using Kaizen.Entities.UserKins;
using Kaizen.Entities.UserKins.Dto;

namespace Kaizen.Entities.Notes.Dto
{
    public class UserKinMapProfile : Profile
    {
        public UserKinMapProfile()
        {
            CreateMap<UserKinDto, UserKin>();
            CreateMap<UserKin, UserKinDto>();

            CreateMap<CreateUserKinDto, UserKin>();
        }
    }
}