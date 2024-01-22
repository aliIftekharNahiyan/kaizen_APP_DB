using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Kaizen.Entities.Lookups.Dto;
using Kaizen.Enums;

namespace Kaizen.Entities.Lookups
{
    public class ClientSupportAppService : AsyncCrudAppService<Lookup, LookupDto, long, PagedLookupResultRequestDto, CreateLookupDto, LookupDto>, ILookupAppService
    {
        public ClientSupportAppService(
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
                .Where(x => x.IsActive == true && x.LookTypeId == Convert.ToInt64(CommonEnum.LookupType.RegularClientSupport));
        }
        
    }
}