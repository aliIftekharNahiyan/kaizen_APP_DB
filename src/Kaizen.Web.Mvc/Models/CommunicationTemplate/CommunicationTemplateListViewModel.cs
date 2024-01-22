
using System.Collections.Generic;
using Kaizen.Entities.CommunicationTemplates.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaizen.Web.Models.CommunicationTemplate
{
    public class CommunicationTemplateListViewModel
    {
        public IReadOnlyList<CommunicationTemplateDto> Entities { get; set; }
    }
}