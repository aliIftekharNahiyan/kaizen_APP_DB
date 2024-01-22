
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.Checklists.Dto
{
    public class PagedChecklistResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}