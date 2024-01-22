using Abp.Application.Services;
using Kaizen.Entities.Addresses.Dto;

namespace Kaizen.Entities.Addresses
{
    public interface IAddressAppService : IAsyncCrudAppService<AddressDto, long, PagedAddressResultRequestDto, CreateAddressDto, AddressDto>
    {

    }
}
