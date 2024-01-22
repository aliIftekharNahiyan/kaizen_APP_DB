
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.SessionGroups.Dto
{
    public class PagedSessionGroupResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
        public long? UserId { get; set; }
    }
}