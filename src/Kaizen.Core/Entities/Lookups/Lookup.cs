using Kaizen.Entities.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Kaizen.Entities.Lookups
{
    [Audited]
    public class Lookup : Entity<long>
    {
       
        public long LookTypeId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string SName { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; } = false;

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
     
    }
}
