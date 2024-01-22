using Abp.Application.Services.Dto;
using System;

namespace Kaizen.Entities.Addresses.Dto
{
    //custom PagedResultRequestDto
    public class PagedAddressResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
        public bool? IsPrimary { get; set; }
        public long? UserId { get; set; }
    }
}
