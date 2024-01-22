
            using AutoMapper;
            using Kaizen.Entities.RegionLocations;
            using Kaizen.Entities.RegionLocations.Dto;

            namespace Kaizen.Users.Dto
            {
                public class RegionLocationMapProfile : Profile
                {
                    public RegionLocationMapProfile()
                    {
                        CreateMap<RegionLocation, RegionLocationDto>();
                        CreateMap<RegionLocation, CreateRegionLocationDto>();

                        CreateMap<CreateRegionLocationDto, RegionLocation>();
                    }
                }
            }