using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Auditing;
using Kaizen.Authorization.Users;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Entities.UserClientSupports
{
    [Audited]
    public class UserClientSupport : Entity<long>
    {
     
        public long UserId { get; set; }

        public long ClientId { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; }=false;

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

    
        [ForeignKey(nameof(UserId))]
        public virtual User Users { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual User Clients { get; set; }

       
    }
}
