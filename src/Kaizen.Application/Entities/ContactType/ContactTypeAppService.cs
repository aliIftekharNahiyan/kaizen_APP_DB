using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;

using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Kaizen.Entities.Lookups.Dto;
using Kaizen.Entities.Lookups;
using Abp.Extensions;

namespace Kaizen.Entities.ContactTypes
{
    public class ContactTypeAppService : AsyncCrudAppService<Lookup, LookupDto, long, PagedLookupResultRequestDto, CreateLookupDto, LookupDto>, ILookupAppService
    {
        public ContactTypeAppService(
            IRepository<Lookup, long> repository)
            : base(repository)
        {
        }
        

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
                .Where(x => x.IsActive == true && x.LookTypeId == 4);
        }
        
    }
}