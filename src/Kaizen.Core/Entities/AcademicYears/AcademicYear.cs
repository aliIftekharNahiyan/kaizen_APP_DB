using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;
using Kaizen.Entities.AcademicTerms;
using Kaizen.Entities.Companies;
using Kaizen.Entities.LinkTables;

namespace Kaizen.Entities.AcademicYears
{
    [Audited]
    public class AcademicYear : Entity<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool Archived { get; set; } = false;

        public long? CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public virtual Company Company { get; set; }

        public virtual List<AcademicTerm> AcademicTerms { get; set; }

    }
}
