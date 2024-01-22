using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.Universitys.Dto;
using Abp.Linq.Extensions;

namespace Kaizen.Entities.Universitys
{
    public class UniversityAppService : AsyncCrudAppService<University, UniversityDto, long, PagedUniversityResultRequestDto, CreateUniversityDto, UniversityDto>, IUniversityAppService
    {
        public UniversityAppService(
            IRepository<University, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<University> ApplySorting(IQueryable<University> query, PagedUniversityResultRequestDto input)
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

        protected override IQueryable<University> CreateFilteredQuery(PagedUniversityResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(keywordLower));
        }
    }
}