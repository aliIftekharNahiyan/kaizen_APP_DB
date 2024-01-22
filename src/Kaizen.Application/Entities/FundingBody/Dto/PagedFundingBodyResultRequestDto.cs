
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.FundingBodys.Dto
{
    public class PagedFundingBodyResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}