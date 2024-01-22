using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.RegionLocations.Dto;


namespace Kaizen.Entities.RegionLocations
{
    public class WorkRegionLocationAppService : AsyncCrudAppService<RegionLocation, RegionLocationDto, long, PagedRegionLocationResultRequestDto, CreateRegionLocationDto, RegionLocationDto>, IRegionLocationAppService
    {
        public WorkRegionLocationAppService(
            IRepository<RegionLocation, long> repository)
            : base(repository)
        {
        }


        protected override IQueryable<RegionLocation> ApplySorting(IQueryable<RegionLocation> query, PagedRegionLocationResultRequestDto input)
        {
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                return query.OrderBy(input.Sorting);
            }

            return base.ApplySorting(query, input);
        }

        protected override IQueryable<RegionLocation> CreateFilteredQuery(PagedRegionLocationResultRequestDto input)
        {
            

            return Repository.GetAllIncluding()
                
                .Where(x => x.Deleted == false); 
        }
    }
}