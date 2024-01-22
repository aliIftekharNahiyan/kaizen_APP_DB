using AutoMapper;
using Kaizen.Authorization.Users;
using Kaizen.Entities.Addresses;
using Kaizen.Entities.Addresses.Dto;

namespace Kaizen.Users.Dto
{
    public class AddressMapProfile : Profile
    {
        public AddressMapProfile()
        {
            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto>();

            CreateMap<CreateAddressDto, Address>();
        }
    }
}
