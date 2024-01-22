using Abp.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;
using Kaizen.Entities.Lookups;

namespace Kaizen.Entities.Feedbacks
{
    [Audited]
    public class Feedback : Entity<long>
    {     

        public long? LookUpId { get; set; }

        public long? UserId { get; set; }

        [MaxLength]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; } = false;

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
     

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }


        [ForeignKey(nameof(LookUpId))]
        public virtual Lookup Lookup { get; set; }
    }
}
