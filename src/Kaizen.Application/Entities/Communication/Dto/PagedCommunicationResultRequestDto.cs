
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.Communications.Dto
{
    public class PagedCommunicationRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}