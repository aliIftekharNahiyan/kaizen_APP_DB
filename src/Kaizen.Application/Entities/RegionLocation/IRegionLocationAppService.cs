
            using Abp.Application.Services;
            using Kaizen.Entities.RegionLocations.Dto;

            namespace Kaizen.Entities.RegionLocations
            {
                public interface IRegionLocationAppService : IAsyncCrudAppService<RegionLocationDto, long, PagedRegionLocationResultRequestDto, CreateRegionLocationDto, RegionLocationDto>
                {

                }
            }