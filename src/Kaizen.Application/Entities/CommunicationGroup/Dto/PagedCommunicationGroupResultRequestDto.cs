
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.CommunicationGroups.Dto
{
    public class PagedCommunicationGroupRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}