using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.Regions.Dto;
using Abp.Linq.Extensions;

namespace Kaizen.Entities.Regions
{
    public class RegionAppService : AsyncCrudAppService<Region, RegionDto, long, PagedRegionResultRequestDto, CreateRegionDto, RegionDto>, IRegionAppService
    {
        public RegionAppService(
            IRepository<Region, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<Region> ApplySorting(IQueryable<Region> query, PagedRegionResultRequestDto input)
        {
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                return query.OrderBy(input.Sorting);
            } 
            else
            {
                return query.OrderBy("name asc");
            }
        }

        protected override IQueryable<Region> CreateFilteredQuery(PagedRegionResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(keywordLower));
        }
    }
}