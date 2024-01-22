
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.ChecklistItems.Dto
{
    public class PagedChecklistItemResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}