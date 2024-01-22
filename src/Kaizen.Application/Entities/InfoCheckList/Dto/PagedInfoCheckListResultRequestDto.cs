
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.InfoCheckLists.Dto
{
    public class PagedInfoCheckListRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}