using Kaizen.Entities.CommunicationTemplates.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Kaizen.Web.Models.CommunicationTemplate
{
    public class CommunicationTemplateBaseViewModel
    {
        public List<SelectListItem> HeaderTemplates { get; set; }
        public List<SelectListItem> FooterTemplates { get; set; }
        public List<SelectListItem> ContentTemplates { get; set; }

        public IFormFile HTMLFileUpload { get; set; }
    }
}