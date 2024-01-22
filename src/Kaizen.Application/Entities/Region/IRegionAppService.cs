
using Abp.Application.Services;
using Kaizen.Entities.Regions.Dto;

namespace Kaizen.Entities.Regions
{
    public interface IRegionAppService : IAsyncCrudAppService<RegionDto, long, PagedRegionResultRequestDto, CreateRegionDto, RegionDto>
    {

    }
}