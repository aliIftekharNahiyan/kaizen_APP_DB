using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Domain.Entities;

namespace Kaizen.Entities.Universitys
{
    [Audited]
    public class University : Entity<long>
    {
        [Required]
        [StringLength(512)]
        public string Name { get; set; }

        [Required]
        public bool Deleted { get; set; } = false;
    }
}
