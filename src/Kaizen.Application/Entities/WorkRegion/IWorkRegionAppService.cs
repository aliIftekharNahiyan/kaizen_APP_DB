using Abp.Application.Services;
using Kaizen.Entities.Lookups.Dto;
using System.Linq;
using System;
using Kaizen.Entities.RegionLocations.Dto;

namespace Kaizen.Entities.Relations
{
    public interface IWorkRegionAppService : IAsyncCrudAppService<RegionLocationDto, long, PagedRegionLocationResultRequestDto, CreateRegionLocationDto, RegionLocationDto>
    {
        // public IQueryable<Lookup> GetManyQueryable(Func<Lookup, bool> where);
    }
}