
using AutoMapper;
using Kaizen.Entities.CustomerTypes;
using Kaizen.Entities.CustomerTypes.Dto;

namespace Kaizen.Users.Dto
{
    public class CustomerTypeMapProfile : Profile
    {
        public CustomerTypeMapProfile()
        {
            CreateMap<CustomerType, CustomerTypeDto>();
            CreateMap<CustomerType, CreateCustomerTypeDto>();

            CreateMap<CreateCustomerTypeDto, CustomerType>();
        }
    }
}