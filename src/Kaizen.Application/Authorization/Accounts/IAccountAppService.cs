using System.Threading.Tasks;
using Abp.Application.Services;
using Kaizen.Authorization.Accounts.Dto;

namespace Kaizen.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
