using Abp.Application.Services.Dto;

namespace Kaizen.Entities.BulkProcessJobs.Dto
{
    public class PagedBulkProcessJobResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}