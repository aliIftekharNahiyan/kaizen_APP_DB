using Abp.Application.Services.Dto;

namespace Kaizen.Entities.Skills.Dto
{

    public class PagedSkillRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}