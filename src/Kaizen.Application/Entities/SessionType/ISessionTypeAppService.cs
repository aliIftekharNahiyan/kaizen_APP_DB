using Abp.Application.Services;
using Kaizen.Entities.SessionTypes.Dto;

namespace Kaizen.Entities.SessionTypes
{
    public interface ISessionTypeAppService : IAsyncCrudAppService<SessionTypeDto, long, PagedSessionTypeResultRequestDto, CreateSessionTypeDto, SessionTypeDto>
    {

    }
}