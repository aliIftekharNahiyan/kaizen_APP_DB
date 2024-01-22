using Abp.Application.Services;
using Kaizen.Entities.BulkProcessJobs.Dto;
using Kaizen.Entities.Companies.Dto;

namespace Kaizen.Entities.Companies
{
    public interface IBulkProcessJobAppService : IAsyncCrudAppService<BulkProcessJobDto, long, PagedBulkProcessJobResultRequestDto, CreateBulkProcessJobDto, BulkProcessJobDto>
    {

    }
}