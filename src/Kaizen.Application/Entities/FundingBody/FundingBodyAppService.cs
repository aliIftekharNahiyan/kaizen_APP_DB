
using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.FundingBodys.Dto;
using Abp.Linq.Extensions;

namespace Kaizen.Entities.FundingBodys
{
    public class FundingBodyAppService : AsyncCrudAppService<FundingBody, FundingBodyDto, long, PagedFundingBodyResultRequestDto, CreateFundingBodyDto, FundingBodyDto>, IFundingBodyAppService
    {
        public FundingBodyAppService(
            IRepository<FundingBody, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<FundingBody> ApplySorting(IQueryable<FundingBody> query, PagedFundingBodyResultRequestDto input)
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

        protected override IQueryable<FundingBody> CreateFilteredQuery(PagedFundingBodyResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding()
                 .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(keywordLower));
        }
    }
}