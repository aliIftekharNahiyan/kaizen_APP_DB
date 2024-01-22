using Abp.Application.Services;
using Kaizen.MultiTenancy.Dto;

namespace Kaizen.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

