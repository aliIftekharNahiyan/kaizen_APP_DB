
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Kaizen.Entities.SupportSessions.Dto;
using System.Threading.Tasks;

namespace Kaizen.Entities.SupportSessions
{
    public interface ISupportSessionAppService : IAsyncCrudAppService<SupportSessionDto, long, PagedSupportSessionResultRequestDto, CreateSupportSessionDto, SupportSessionDto>
    {
        Task<PagedResultDto<SupportSessionCalendarDto>> GetAllForCalendar(PagedSupportSessionResultRequestDto input);
    }
}