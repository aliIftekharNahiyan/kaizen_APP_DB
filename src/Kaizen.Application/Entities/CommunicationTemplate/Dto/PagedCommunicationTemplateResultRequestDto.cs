
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.CommunicationTemplates.Dto
{
    public class PagedCommunicationTemplateRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}