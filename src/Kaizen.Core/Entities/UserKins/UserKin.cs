using Abp.Authorization.Users;
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
using Kaizen.Authorization.Users;

namespace Kaizen.Entities.UserKins
{
    [Audited]
    public class UserKin:Entity<long>
    {
     
        public long UserId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        public long? RelationId { get; set; }

        public long? ContactTypeId { get; set; }

        [MaxLength(100)]
        public string Contact { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }=false;

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

    
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(ContactTypeId))]
        public virtual Lookup ContactType { get; set; }

        [ForeignKey(nameof(RelationId))]
        public virtual Lookup Relation { get; set; }
    }
}
