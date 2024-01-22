using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Domain.Entities;

namespace Kaizen.Entities.FundingBodys
{
    [Audited]
    public class FundingBody : Entity<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public bool Deleted { get; set; } = false;
    }
}
