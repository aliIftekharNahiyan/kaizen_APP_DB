
using AutoMapper;

namespace Kaizen.Entities.Companies.Dto
{
    public class CompanyMapProfile : Profile
    {
        public CompanyMapProfile()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<Company, CreateCompanyDto>();
            CreateMap<CreateCompanyDto, Company>();
        }
    }
}