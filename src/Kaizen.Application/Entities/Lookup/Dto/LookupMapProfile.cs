
using AutoMapper;
using Kaizen.Entities.Lookups;
using Kaizen.Entities.Lookups.Dto;

namespace Kaizen.Entities.Lookups.Dto
{
    public class LookupMapProfile : Profile
    {
        public LookupMapProfile()
        {
            CreateMap<Lookup, LookupDto>().ReverseMap();
            CreateMap<Lookup, CreateLookupDto>().ReverseMap();
            //CreateMap<CreateLookTypeDto, LookType>();
        }
    }
}