using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;
using Kaizen.Entities.AcademicYears;
using Kaizen.Entities.Companies;

namespace Kaizen.Entities.AcademicTerms
{
    [Audited]
    public class AcademicTerm : Entity<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(512)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool Archived { get; set; } = false;

        public long AcademicYearId { get; set; }

        [ForeignKey(nameof(AcademicYearId))]
        public virtual AcademicYear AcademicYear { get; set; }

    }
}
