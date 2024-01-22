
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.CustomerTypes.Dto
{
    public class PagedCustomerTypeResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}