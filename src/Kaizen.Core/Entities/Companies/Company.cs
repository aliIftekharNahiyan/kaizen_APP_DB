using Abp.Auditing;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Entities.Companies
{
    [Audited]
    public class Company : Entity<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(512)]
        public string Description { get; set; }

        [Required]
        [StringLength(256)]        
        public string SendFromEmailAddress { get; set; }               
       
        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public bool Deleted { get; set; } = false;
        
        [Required]
        public long LogoFileId { get; set; }
     
        [StringLength(8)]
        public string PrimaryBrandColour { get; set; }

        [StringLength(8)]
        public string SecondaryBrandColour { get; set; }

    }
}
