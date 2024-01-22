using AutoMapper;
using Kaizen.Entities.SupportTypes;
using Kaizen.Entities.SupportTypes.Dto;

namespace Kaizen.Users.Dto
{
    public class SupportTypeMapProfile : Profile
    {
        public SupportTypeMapProfile()
        {
            CreateMap<SupportTypeDto, SupportType>();
            CreateMap<SupportType, SupportTypeDto>();

            CreateMap<CreateSupportTypeDto, SupportType>();
        }
    }
}
