
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.Regions.Dto
{
    public class PagedRegionResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}