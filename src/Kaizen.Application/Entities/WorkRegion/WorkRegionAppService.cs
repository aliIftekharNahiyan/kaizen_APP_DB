using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Kaizen.Entities.RegionLocations;
using Kaizen.Entities.RegionLocations.Dto;
using Kaizen.Entities.Relations;

namespace Kaizen.Entities.WorkRegion
{
    public class WorkRegionAppService : AsyncCrudAppService<RegionLocation, RegionLocationDto, long, PagedRegionLocationResultRequestDto, CreateRegionLocationDto, RegionLocationDto>, IWorkRegionAppService
    {
        public WorkRegionAppService(
            IRepository<RegionLocation, long> repository)
            : base(repository)
        {
        }



        //public IQueryable<Lookup> GetManyQueryable(Func<Lookup, bool> where)
        //{
        //    IQueryable<Lookup> lookup = null;
        //    lookup =  Repository.GetAll().Where(where).AsQueryable();
        //    return lookup;
        //}
        //public override async Task<PagedResultDto<LookupDto>> GetAllAsync(PagedUserKinResultRequestDto input)
        //{

        //    var query = Repository.GetAllIncluding().Where(x => x.IsActive == true && x.LookTypeId==5);

        //    var ee = new PagedResultDto<LookupDto> { Items = ObjectMapper.Map<List<LookupDto>>(query) };
        //    return ee;

        //}

        protected override IQueryable<RegionLocation> ApplySorting(IQueryable<RegionLocation> query, PagedRegionLocationResultRequestDto input)
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
        protected override IQueryable<RegionLocation> CreateFilteredQuery(PagedRegionLocationResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding()
                 .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(keywordLower))
                 .Where(x => x.Deleted == false);
        }
        
    }
}