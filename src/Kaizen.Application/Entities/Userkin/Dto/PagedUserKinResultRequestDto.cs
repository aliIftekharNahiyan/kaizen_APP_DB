using Abp.Application.Services.Dto;
using System;

namespace Kaizen.Entities.UserKins.Dto
{
    //custom PagedResultRequestDto
    public class PagedUserKinResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
        public string EntityType { get; set; }
        public long EntityId { get; set; }
    }
}