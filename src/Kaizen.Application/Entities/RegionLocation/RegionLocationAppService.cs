using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.RegionLocations.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Kaizen.Authorization.Users;
using Kaizen.Users.Dto;

namespace Kaizen.Entities.RegionLocations
{
    public class RegionLocationAppService : AsyncCrudAppService<RegionLocation, RegionLocationDto, long, PagedRegionLocationResultRequestDto, CreateRegionLocationDto, RegionLocationDto>, IRegionLocationAppService
    {
        public RegionLocationAppService(
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
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding();
        }
    }
}