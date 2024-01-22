using Abp.Application.Services.Dto;
using System;

namespace Kaizen.Entities.SupportTypes.Dto
{
    //custom PagedResultRequestDto
    public class PagedSupportTypeResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
        public long? UserId { get; set; }
    }
}
