
using AutoMapper;
using Kaizen.Entities.FundingBodys;
using Kaizen.Entities.FundingBodys.Dto;

namespace Kaizen.Users.Dto
{
    public class FundingBodyMapProfile : Profile
    {
        public FundingBodyMapProfile()
        {
            CreateMap<FundingBody, FundingBodyDto>();
            CreateMap<FundingBody, CreateFundingBodyDto>();

            CreateMap<CreateFundingBodyDto, FundingBody>();
        }
    }
}