using Kaizen.Entities.Communications.Dto;
using Kaizen.Entities.CommunicationTemplates.Dto;
using Kaizen.Web.Models.CommunicationTemplate;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Web.Models.Communication
{
    public class CreateCommunicationViewModel : CommunicationTemplateBaseViewModel
    {
        public CreateCommunicationDto Communication { get; set; }

        [Display(Name = "Content Template")]
        public long? ContentTemplateId { get; set; }

        [Display(Name = "Footer Template")]
        public long? FooterTemplateId { get; set; }

        [Display(Name = "Header Template")]
        public long? HeaderTemplateId { get; set; }
    }
}