
using AutoMapper;
using Kaizen.Entities.Regions;
using Kaizen.Entities.Regions.Dto;

namespace Kaizen.Users.Dto
{
    public class RegionMapProfile : Profile
    {
        public RegionMapProfile()
        {
            CreateMap<Region, RegionDto>();
            CreateMap<Region, CreateRegionDto>();

            CreateMap<CreateRegionDto, Region>();
        }
    }
}