using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Kaizen.Entities.Addresses.Dto;
using Kaizen.Entities.History.Dto;

namespace Kaizen.Entities.History
{
    public interface IHistoryAppService : IApplicationService
    {
        PagedResultDto<HistoryChangesetDto> GetChangesetsForEntity(PagedHistoryResultRequestDto filter);
        PagedResultDto<HistoryDto> GetForChangeset(PagedHistoryResultRequestDto filter);
    }
}
