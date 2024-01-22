using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Enums;

namespace Kaizen.Entities.CommunicationTemplates.Dto
{
    [AutoMapTo(typeof(CommunicationTemplate))]
    public class CreateCommunicationTemplateDto : EntityDto<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Display(Name = "Company")]
        public long? CompanyId { get; set; }

        [Display(Name = "Header Template")]
        public long? HeaderTemplateId { get; set; }

        [Display(Name = "Footer Template")]
        public long? FooterTemplateId { get; set; }

        [Display(Name = "Content")]
        public string TemplateHTMLContentUrl { get; set; }

        [Required]
        public string HTMLContent { get; set; }

        [Display(Name = "Template Area")]
        [Required]
        public CommunicationTemplateArea TemplateArea { get; set; }

        [Display(Name = "Template Type")]
        [Required]
        public CommunicationTemplateType TemplateType { get; set; }
    }
}