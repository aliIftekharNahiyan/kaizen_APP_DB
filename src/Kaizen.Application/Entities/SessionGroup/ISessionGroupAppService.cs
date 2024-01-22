
            using Abp.Application.Services;
            using Kaizen.Entities.SessionGroups.Dto;

            namespace Kaizen.Entities.SessionGroups
            {
                public interface ISessionGroupAppService : IAsyncCrudAppService<SessionGroupDto, long, PagedSessionGroupResultRequestDto, CreateSessionGroupDto, SessionGroupDto>
                {

                }
            }