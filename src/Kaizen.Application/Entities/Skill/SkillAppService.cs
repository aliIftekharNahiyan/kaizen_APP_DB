using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Kaizen.Entities.Skills.Dto;

namespace Kaizen.Entities.Skills
{
    public class SkillAppService : AsyncCrudAppService<Skill, SkillDto, long, PagedSkillRequestDto, CreateSkillDto, SkillDto>, ISkillAppService
    {
        
        public SkillAppService(
            IRepository<Skill, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<Skill> ApplySorting(IQueryable<Skill> query, PagedSkillRequestDto input)
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
        protected override IQueryable<Skill> CreateFilteredQuery(PagedSkillRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(keywordLower))
                .Where(x => x.IsActive == true );
        }
    }
}