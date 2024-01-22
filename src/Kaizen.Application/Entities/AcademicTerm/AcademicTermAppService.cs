
using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.FundingBodys.Dto;
using Abp.Linq.Extensions;
using Kaizen.Entities.AcademicTerms.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Kaizen.MultiTenancy;
using Abp.UI;
using Kaizen.Entities.SessionGroups.Dto;
using System.Collections.Generic;

namespace Kaizen.Entities.AcademicTerms
{
    public class AcademicTermAppService : AsyncCrudAppService<AcademicTerm, AcademicTermDto, long, PagedAcademicTermRequestDto, CreateAcademicTermDto, AcademicTermDto>, IAcademicTermAppService
    {
        public AcademicTermAppService(
            IRepository<AcademicTerm, long> repository)
            : base(repository)
        {
        }

        public override async Task<AcademicTermDto> GetAsync(EntityDto<long> input)
        {
            AcademicTerm academicTerm = await Repository.GetAllIncluding(a => a.AcademicYear).SingleOrDefaultAsync(a => a.Id == input.Id);

            return ObjectMapper.Map<AcademicTermDto>(academicTerm);
        }


        public override async Task<PagedResultDto<AcademicTermDto>> GetAllAsync(PagedAcademicTermRequestDto input)
        {
            var query = Repository.GetAllIncluding(a => a.AcademicYear);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            return new PagedResultDto<AcademicTermDto> { Items = ObjectMapper.Map<List<AcademicTermDto>>(query), TotalCount = totalCount };
        }

        protected override IQueryable<AcademicTerm> ApplySorting(IQueryable<AcademicTerm> query, PagedAcademicTermRequestDto input)
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

        protected override IQueryable<AcademicTerm> CreateFilteredQuery(PagedAcademicTermRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding()
                 .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(keywordLower));
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            CheckDeletePermission();

            throw new UserFriendlyException("An Academic Term cannot be deleted");
        }
    }
}