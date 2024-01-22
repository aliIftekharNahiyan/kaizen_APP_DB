using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Kaizen.Entities.BulkProcess;
using Kaizen.Entities.BulkProcessJobs.Dto;
using Kaizen.Entities.Companies;
using Kaizen.Entities.Companies.Dto;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Kaizen.Entities.BulkProcessJobs
{
    public class BulkProcessJobAppService : AsyncCrudAppService<BulkProcessJob, BulkProcessJobDto, long, PagedBulkProcessJobResultRequestDto, CreateBulkProcessJobDto, BulkProcessJobDto>, IBulkProcessJobAppService
    {
        public BulkProcessJobAppService(
            IRepository<BulkProcessJob, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<BulkProcessJob> ApplySorting(IQueryable<BulkProcessJob> query, PagedBulkProcessJobResultRequestDto input)
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

        protected override IQueryable<BulkProcessJob> CreateFilteredQuery(PagedBulkProcessJobResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding()
                 .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.FileUploadedUrl.ToLower().Contains(keywordLower));
        }


    }
}