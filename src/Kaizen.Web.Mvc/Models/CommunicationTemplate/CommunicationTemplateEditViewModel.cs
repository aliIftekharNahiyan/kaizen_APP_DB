using Kaizen.Entities.CommunicationTemplates.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Web.Models.CommunicationTemplate
{
    public class CommunicationTemplateEditViewModel : CommunicationTemplateBaseViewModel
    {
        public CommunicationTemplateDto CommunicationTemplate { get; set; }
    }
}