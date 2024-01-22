
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.SessionTypes.Dto
{
    public class PagedSessionTypeResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}