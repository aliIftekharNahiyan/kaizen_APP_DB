
using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.FundingBodys.Dto;
using Abp.Linq.Extensions;
using Kaizen.Entities.InfoCheckLists.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Kaizen.MultiTenancy;
using Abp.UI;
using Kaizen.Entities.SessionGroups.Dto;
using System.Collections.Generic;
using Kaizen.Entities.InfoChecklists;

namespace Kaizen.Entities.InfoCheckLists
{
    public class InfoCheckListAppService : AsyncCrudAppService<InfoCheckList, InfoCheckListDto, long, PagedInfoCheckListRequestDto, CreateInfoCheckListDto, InfoCheckListDto>, IInfoCheckListAppService
    {
        public InfoCheckListAppService(
            IRepository<InfoCheckList, long> repository)
            : base(repository)
        {
        }

        public override async Task<InfoCheckListDto> GetAsync(EntityDto<long> input)
        {
            InfoCheckList infoCheckList = await Repository.GetAllIncluding(a => a.User).SingleOrDefaultAsync(a => a.Id == input.Id);

            return ObjectMapper.Map<InfoCheckListDto>(infoCheckList);
        }


        public override async Task<PagedResultDto<InfoCheckListDto>> GetAllAsync(PagedInfoCheckListRequestDto input)
        {
            var query = Repository.GetAllIncluding(a => a.User);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            return new PagedResultDto<InfoCheckListDto> { Items = ObjectMapper.Map<List<InfoCheckListDto>>(query), TotalCount = totalCount };
        }

        protected override IQueryable<InfoCheckList> ApplySorting(IQueryable<InfoCheckList> query, PagedInfoCheckListRequestDto input)
        {
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                return query.OrderBy(input.Sorting);
            }
            else
            {
                return query.OrderBy("firstName asc");
            }
        }

        //protected override IQueryable<InfoCheckList> CreateFilteredQuery(PagedInfoCheckListRequestDto input)
        //{
        //    var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

        //    return Repository.GetAllIncluding()
        //         .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(keywordLower));
        //}

        public override Task<InfoCheckListDto> CreateAsync(CreateInfoCheckListDto input)
        {
            input.DateAnswered = DateTime.UtcNow;
            input.UserId = 5;

            return base.CreateAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            CheckDeletePermission();

            throw new UserFriendlyException("Info check list cannot be deleted");
        }
    }
}