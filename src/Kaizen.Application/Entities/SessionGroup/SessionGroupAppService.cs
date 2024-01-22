using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.SessionGroups.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using Abp.Linq.Extensions;
using System.Xml.Linq;

namespace Kaizen.Entities.SessionGroups
{
    public class SessionGroupAppService : AsyncCrudAppService<SessionGroup, SessionGroupDto, long, PagedSessionGroupResultRequestDto, CreateSessionGroupDto, SessionGroupDto>, ISessionGroupAppService
    {
        public SessionGroupAppService(
            IRepository<SessionGroup, long> repository)
            : base(repository)
        {
        }

        public override async Task<PagedResultDto<SessionGroupDto>> GetAllAsync(PagedSessionGroupResultRequestDto input)
        {
            var query = Repository.GetAllIncluding(a => a.FundingBody, a => a.SupportType);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            return new PagedResultDto<SessionGroupDto> { Items = ObjectMapper.Map<List<SessionGroupDto>>(query), TotalCount = totalCount };
        }


        public override Task<SessionGroupDto> CreateAsync(CreateSessionGroupDto input)
        {
            input.CreationDate = DateTime.UtcNow;
            input.ExpiryDate = input.ExpiryDate.ToUniversalTime();

            return base.CreateAsync(input);
        }

        protected override IQueryable<SessionGroup> ApplySorting(IQueryable<SessionGroup> query, PagedSessionGroupResultRequestDto input)
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

        protected override IQueryable<SessionGroup> CreateFilteredQuery(PagedSessionGroupResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding(a => a.SupportType, a => a.FundingBody)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(keywordLower))
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Description.ToLower().Contains(keywordLower))
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.FundingBody.Name.ToLower().Contains(keywordLower))
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.SupportType.Name.ToLower().Contains(keywordLower))
                .WhereIf(input.UserId.HasValue, x => x.UserId == input.UserId);
        }
    }
}