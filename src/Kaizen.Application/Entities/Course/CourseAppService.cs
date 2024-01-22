
using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.FundingBodys.Dto;
using Abp.Linq.Extensions;
using Kaizen.Entities.Courses.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Kaizen.MultiTenancy;
using Abp.UI;
using Kaizen.Entities.SessionGroups.Dto;
using System.Collections.Generic;

namespace Kaizen.Entities.Courses
{
    public class CourseAppService : AsyncCrudAppService<Course, CourseDto, long, PagedCourseRequestDto, CreateCourseDto, CourseDto>, ICourseAppService
    {
        public CourseAppService(
            IRepository<Course, long> repository)
            : base(repository)
        {
        }

        public override async Task<CourseDto> GetAsync(EntityDto<long> input)
        {
            Course academicTerm = await Repository.GetAllIncluding(a => a.University).SingleOrDefaultAsync(a => a.Id == input.Id);

            return ObjectMapper.Map<CourseDto>(academicTerm);
        }


        public override async Task<PagedResultDto<CourseDto>> GetAllAsync(PagedCourseRequestDto input)
        {
            var query = Repository.GetAllIncluding(a => a.University).Where(x => x.UniversityId == input.UniversityId);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            return new PagedResultDto<CourseDto> { Items = ObjectMapper.Map<List<CourseDto>>(query), TotalCount = totalCount };
        }

        protected override IQueryable<Course> ApplySorting(IQueryable<Course> query, PagedCourseRequestDto input)
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

        protected override IQueryable<Course> CreateFilteredQuery(PagedCourseRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding()
                 .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(keywordLower));
        }

    }
}