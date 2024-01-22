
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Kaizen.Entities.SessionTypes.Dto;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Kaizen.Entities.SessionTypes
{
    public class SessionTypeAppService : AsyncCrudAppService<SessionType, SessionTypeDto, long, PagedSessionTypeResultRequestDto, CreateSessionTypeDto, SessionTypeDto>, ISessionTypeAppService
    {
        public SessionTypeAppService(
            IRepository<SessionType, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<SessionType> ApplySorting(IQueryable<SessionType> query, PagedSessionTypeResultRequestDto input)
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

        protected override IQueryable<SessionType> CreateFilteredQuery(PagedSessionTypeResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding()
                 .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(keywordLower));
        }
    }
}