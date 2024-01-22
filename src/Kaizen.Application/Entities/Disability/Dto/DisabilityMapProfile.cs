
            using AutoMapper;
            using Kaizen.Entities.Disabilitys;
            using Kaizen.Entities.Disabilitys.Dto;

            namespace Kaizen.Users.Dto
            {
                public class DisabilityMapProfile : Profile
                {
                    public DisabilityMapProfile()
                    {
                        CreateMap<Disability, DisabilityDto>();
                        CreateMap<Disability, CreateDisabilityDto>();

                        CreateMap<CreateDisabilityDto, Disability>();
                    }
                }
            }