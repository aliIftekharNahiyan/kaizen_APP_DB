using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Domain.Entities;

namespace Kaizen.Entities.ContactMethods
{
    [Audited]
    public class ContactMethod : Entity<long>
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public bool Deleted { get; set; } = false;
    }
}
