using Abp.Application.Services;
using Kaizen.Entities.Addresses.Dto;
using Kaizen.Entities.SupportTypes.Dto;

namespace Kaizen.Entities.SupportTypes
{
    public interface ISupportTypeAppService : IAsyncCrudAppService<SupportTypeDto, long, PagedSupportTypeResultRequestDto, CreateSupportTypeDto, SupportTypeDto>
    {

    }
}
