
using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.FundingBodys.Dto;
using Abp.Linq.Extensions;
using Kaizen.Entities.Communications.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Kaizen.MultiTenancy;
using Abp.UI;
using Kaizen.Entities.SessionGroups.Dto;
using System.Collections.Generic;

namespace Kaizen.Entities.Communications
{
    public class CommunicationAppService : AsyncCrudAppService<Communication, CommunicationDto, long, PagedCommunicationRequestDto, CreateCommunicationDto, CommunicationDto>, ICommunicationAppService
    {
        public CommunicationAppService(
            IRepository<Communication, long> repository)
            : base(repository)
        {
        }

        public override async Task<CommunicationDto> GetAsync(EntityDto<long> input)
        {
            Communication Communication = await Repository.GetAllIncluding().SingleOrDefaultAsync(a => a.Id == input.Id);

            return ObjectMapper.Map<CommunicationDto>(Communication);
        }


        public override async Task<PagedResultDto<CommunicationDto>> GetAllAsync(PagedCommunicationRequestDto input)
        {
            var query = Repository.GetAllIncluding();

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            return new PagedResultDto<CommunicationDto> { Items = ObjectMapper.Map<List<CommunicationDto>>(query), TotalCount = totalCount };
        }

        protected override IQueryable<Communication> ApplySorting(IQueryable<Communication> query, PagedCommunicationRequestDto input)
        {
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                return query.OrderBy(input.Sorting);
            }
            else
            {
                return query.OrderBy("datecreated desc");
            }
        }

        protected override IQueryable<Communication> CreateFilteredQuery(PagedCommunicationRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding();
        }
    }
}