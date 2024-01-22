using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Entities.Companies;
using Kaizen.Entities.LinkTables;
using Kaizen.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Entities.CommunicationTemplates
{
    [Audited]
    public class CommunicationTemplate : Entity<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public long? CompanyId { get; set; }

        public long? HeaderTemplateId { get; set; }

        public long? FooterTemplateId { get; set; }

        public string TemplateHTMLContentUrl { get; set; }

        public CommunicationTemplateType TemplateType { get; set; }

        public CommunicationTemplateArea TemplateArea { get; set; }

        [Required]
        public bool Deleted { get; set; } = false;

        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; }

        [ForeignKey(nameof(HeaderTemplateId))]
        public CommunicationTemplate HeaderTemplate { get; set; }

        [ForeignKey(nameof(FooterTemplateId))]
        public CommunicationTemplate FooterTemplate { get; set; }
    }
}
