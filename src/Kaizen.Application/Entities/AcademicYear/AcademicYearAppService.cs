
using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.FundingBodys.Dto;
using Abp.Linq.Extensions;
using Kaizen.Entities.AcademicYears.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Kaizen.Entities.AcademicTerms;

namespace Kaizen.Entities.AcademicYears
{
    public class AcademicYearAppService : AsyncCrudAppService<AcademicYear, AcademicYearDto, long, PagedAcademicYearRequestDto, CreateAcademicYearDto, AcademicYearDto>, IAcademicYearAppService
    {
        public IRepository<AcademicTerm, long> _academicTermRepository { get; set; }

        public AcademicYearAppService(
            IRepository<AcademicYear, long> repository,
            IRepository<AcademicTerm, long> academicTermRepository)
            : base(repository)
        {
            _academicTermRepository = academicTermRepository;
        }

        public override async Task<AcademicYearDto> GetAsync(EntityDto<long> input)
        {
            AcademicYear academicYear = await Repository.GetAllIncluding(a => a.Company).SingleOrDefaultAsync(a => a.Id == input.Id);

            return ObjectMapper.Map<AcademicYearDto>(academicYear);
        }

        protected override IQueryable<AcademicYear> ApplySorting(IQueryable<AcademicYear> query, PagedAcademicYearRequestDto input)
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

        protected override IQueryable<AcademicYear> CreateFilteredQuery(PagedAcademicYearRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding()
                 .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(keywordLower));
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            CheckDeletePermission();
            bool academicTermAssigned = _academicTermRepository.Count(a => a.AcademicYearId == input.Id) > 0;

            if (academicTermAssigned)
            {
                throw new UserFriendlyException("There is a related Academic Term");
            }
            else
            {
                await base.DeleteAsync(input);
            }
        }
    }
}