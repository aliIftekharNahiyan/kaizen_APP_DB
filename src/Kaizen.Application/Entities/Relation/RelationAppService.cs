using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Kaizen.Entities.Lookups.Dto;
using Kaizen.Entities.Lookups;
using Kaizen.Enums;

namespace Kaizen.Entities.Relations
{
    public class RelationAppService : AsyncCrudAppService<Lookup, LookupDto, long, PagedLookupResultRequestDto, CreateLookupDto, LookupDto>, IRelationAppService
    {
        public RelationAppService(
            IRepository<Lookup, long> repository)
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

        protected override IQueryable<Lookup> ApplySorting(IQueryable<Lookup> query, PagedLookupResultRequestDto input)
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
        protected override IQueryable<Lookup> CreateFilteredQuery(PagedLookupResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding()
                 .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(keywordLower))
                 .Where(x => x.IsActive == true && x.LookTypeId == Convert.ToInt64(CommonEnum.LookupType.RelationType));
        }
        
    }
}