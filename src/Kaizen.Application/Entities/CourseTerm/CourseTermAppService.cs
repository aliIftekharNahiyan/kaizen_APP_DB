
using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.FundingBodys.Dto;
using Abp.Linq.Extensions;
using Kaizen.Entities.CourseTerms.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Kaizen.MultiTenancy;
using Abp.UI;
using Kaizen.Entities.SessionGroups.Dto;
using System.Collections.Generic;
using Kaizen.Entities.Checklists.Dto;
using Kaizen.Authorization.Roles;
using Abp.Configuration;

namespace Kaizen.Entities.CourseTerms
{
    public class CourseTermAppService : AsyncCrudAppService<CourseTerm, CourseTermDto, long, PagedCourseTermRequestDto, CreateCourseTermDto, CourseTermDto>, ICourseTermAppService
    {
        public CourseTermAppService(
            IRepository<CourseTerm, long> repository)
            : base(repository)
        {
        }

        public override async Task<CourseTermDto> GetAsync(EntityDto<long> input)
        {
            CourseTerm academicTerm = await Repository.GetAllIncluding(a => a.Course).SingleOrDefaultAsync(a => a.Id == input.Id);

            return ObjectMapper.Map<CourseTermDto>(academicTerm);
        }


        public override async Task<PagedResultDto<CourseTermDto>> GetAllAsync(PagedCourseTermRequestDto input)
        {
            var query = Repository.GetAllIncluding(a => a.Course).Where(x => x.CourseId == input.CourseId);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            return new PagedResultDto<CourseTermDto> { Items = ObjectMapper.Map<List<CourseTermDto>>(query), TotalCount = totalCount };
        }

        protected override IQueryable<CourseTerm> ApplySorting(IQueryable<CourseTerm> query, PagedCourseTermRequestDto input)
        {
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                return query.OrderBy(input.Sorting);
            }

            return base.ApplySorting(query, input);
        }

        public override async Task<CourseTermDto> UpdateAsync(CourseTermDto input)
        {
            if (input.StartDate.HasValue && input.EndDate.HasValue && !CheckDatesOverlappings(input.Id, input.StartDate.Value, input.EndDate.Value))
            {
                CheckUpdatePermission();

                var entity = Repository.GetAllIncluding().SingleOrDefault(a => a.Id == input.Id);

                MapToEntity(input, entity);
                await CurrentUnitOfWork.SaveChangesAsync();

                return MapToEntityDto(entity);
            }
            else
            {
                throw new UserFriendlyException("Terms must not overlap in dates.");
            }

        }

        public override Task<CourseTermDto> CreateAsync(CreateCourseTermDto input)
        {
            if (input.StartDate.HasValue&&input.EndDate.HasValue&& !CheckDatesOverlappings(input.Id, input.StartDate.Value, input.EndDate.Value))
                return base.CreateAsync(input);
            else
            {
                throw new UserFriendlyException("Terms must not overlap in dates.");
            }
        }

        private bool CheckDatesOverlappings(long id, DateTime StartDate, DateTime EndDate)
        {
            var overlap = false;
            foreach (var term in Repository.GetAllIncluding().Where(x => x.Id != id))
            {
                if (StartDate <= term.EndDate && EndDate >= term.StartDate)
                    overlap = true;
            }

            return overlap;
        }

        protected override IQueryable<CourseTerm> CreateFilteredQuery(PagedCourseTermRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding()
                 .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.LengthOfTerm.ToString().Contains(keywordLower));
        }

    }
}