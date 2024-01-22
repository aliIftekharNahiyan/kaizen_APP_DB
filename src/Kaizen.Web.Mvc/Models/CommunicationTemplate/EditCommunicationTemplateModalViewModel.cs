using Kaizen.Entities.CommunicationTemplates.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Kaizen.Web.Models.CommunicationTemplate
{
    public class EditCommunicationTemplateModalViewModel : CommunicationTemplateBaseViewModel
    {
        public CommunicationTemplateDto CommunicationTemplate { get; set; }
    }
}