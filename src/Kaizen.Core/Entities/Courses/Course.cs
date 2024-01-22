using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Entities.AcademicYears;
using Kaizen.Entities.Universitys;

namespace Kaizen.Entities.Courses
{
    [Audited]
    public class Course : Entity<long>
    {

        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [Required]
        public long UniversityId { get; set; }
        [ForeignKey(nameof(UniversityId))]
        public virtual University University { get; set; }

    }
}
