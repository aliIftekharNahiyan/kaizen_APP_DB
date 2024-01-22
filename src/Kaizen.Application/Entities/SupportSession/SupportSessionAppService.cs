using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.SupportSessions.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using Abp.Linq.Extensions;
using System.Xml.Linq;
using Kaizen.Enums;

namespace Kaizen.Entities.SupportSessions
{
    public class SupportSessionAppService : AsyncCrudAppService<SupportSession, SupportSessionDto, long, PagedSupportSessionResultRequestDto, CreateSupportSessionDto, SupportSessionDto>, ISupportSessionAppService
    {
        public SupportSessionAppService(
            IRepository<SupportSession, long> repository)
            : base(repository)
        {
        }

        public override async Task<PagedResultDto<SupportSessionDto>> GetAllAsync(PagedSupportSessionResultRequestDto input)
        {
            var query = Repository.GetAllIncluding(a => a.FundingBody, a => a.SupportType, a => a.SessionGroup, a => a.SessionType).Where(a => a.UserId == input.UserId.Value);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            return new PagedResultDto<SupportSessionDto> { Items = ObjectMapper.Map<List<SupportSessionDto>>(query), TotalCount = totalCount };
        }

        public async Task<PagedResultDto<SupportSessionCalendarDto>> GetAllForCalendar(PagedSupportSessionResultRequestDto input)
        {
            var query = Repository.GetAllIncluding(a => a.SupportProfessionalUser, a => a.SessionType).Where(a => a.UserId == input.UserId.Value && 
                    a.Status != SupportSessionStatus.Cancelled && 
                    a.Status != SupportSessionStatus.Deleted);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            return new PagedResultDto<SupportSessionCalendarDto> { Items = ObjectMapper.Map<List<SupportSessionCalendarDto>>(query), TotalCount = totalCount };
        }


        public override Task<SupportSessionDto> CreateAsync(CreateSupportSessionDto input)
        {
            input.Status = (int)SupportSessionStatus.Upcoming;
            return base.CreateAsync(input);
        }

        protected override IQueryable<SupportSession> ApplySorting(IQueryable<SupportSession> query, PagedSupportSessionResultRequestDto input)
        {
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                return query.OrderBy(input.Sorting);
            }
            else
            {
                return query.OrderBy("sessionGroup.name asc");
            }
        }

        protected override IQueryable<SupportSession> CreateFilteredQuery(PagedSupportSessionResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding(a => a.SupportType, a => a.FundingBody, a => a.SessionGroup, a => a.SessionType, a => a.SupportProfessionalUser)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.SessionGroup.Name.ToLower().Contains(keywordLower))
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.SupportProfessionalUser.Name.ToLower().Contains(keywordLower))
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.FundingBody.Name.ToLower().Contains(keywordLower))
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.SupportType.Name.ToLower().Contains(keywordLower))
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.SessionType.Name.ToLower().Contains(keywordLower))
                            .WhereIf(input.UserId.HasValue, x => x.UserId == input.UserId);
        }
    }
}