using System.Threading.Tasks;
using Abp.Application.Services;
using Kaizen.Sessions.Dto;

namespace Kaizen.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
