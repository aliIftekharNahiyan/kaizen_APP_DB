using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Kaizen.Entities.Companies.Dto;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Kaizen.Entities.Companies
{
    public class CompanyAppService : AsyncCrudAppService<Company, CompanyDto, long, PagedCompanyResultRequestDto, CreateCompanyDto, CompanyDto>, ICompanyAppService
    {
        public CompanyAppService(
            IRepository<Company, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<Company> ApplySorting(IQueryable<Company> query, PagedCompanyResultRequestDto input)
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

        protected override IQueryable<Company> CreateFilteredQuery(PagedCompanyResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding()
                 .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(keywordLower));
        }


    }
}