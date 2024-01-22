
using AutoMapper;
using Kaizen.Entities.Universitys;
using Kaizen.Entities.Universitys.Dto;

namespace Kaizen.Users.Dto
{
    public class UniversityMapProfile : Profile
    {
        public UniversityMapProfile()
        {
            CreateMap<University, UniversityDto>();
            CreateMap<University, CreateUniversityDto>();

            CreateMap<CreateUniversityDto, University>();
        }
    }
}