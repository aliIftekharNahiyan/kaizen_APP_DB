using Abp.Application.Services.Dto;

namespace Kaizen.Entities.Companies.Dto
{
    public class PagedCompanyResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}