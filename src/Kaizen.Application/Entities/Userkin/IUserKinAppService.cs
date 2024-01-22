using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Entities.UserKins.Dto;

namespace Kaizen.Entities.UserKinAppService
{
    public interface IUserKinAppService : IAsyncCrudAppService<UserKinDto, long, PagedUserKinResultRequestDto, CreateUserKinDto, UserKinDto>
    {
        Task<PagedResultDto<IGrouping<string, UserKinDto>>> GetGroupedAsync(PagedUserKinResultRequestDto input);
    }
}