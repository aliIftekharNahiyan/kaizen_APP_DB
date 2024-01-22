
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.SupportSessions.Dto
{
    public class PagedSupportSessionResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
        public long? UserId { get; set; }
    }
}