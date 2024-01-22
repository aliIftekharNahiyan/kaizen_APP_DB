using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;
using Kaizen.Entities.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Entities.UserLookups
{
    [Audited]
    public class UserLookup : Entity<long>
    {

        [Required]
        public long UserId { get; set; }
        
        [Required]
        public long LookUpId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

    
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
      
        [ForeignKey(nameof(LookUpId))]
        public virtual Lookup Lookup { get; set; }
    }
}
