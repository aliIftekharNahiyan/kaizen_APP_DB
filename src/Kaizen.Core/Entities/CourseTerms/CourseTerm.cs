using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Entities.Courses;

namespace Kaizen.Entities.CourseTerms
{
    [Audited]
    public class CourseTerm : Entity<long>
    {
        [Required]
        public int LengthOfTerm { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        [Required]
        public long CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set; }

    }
}
