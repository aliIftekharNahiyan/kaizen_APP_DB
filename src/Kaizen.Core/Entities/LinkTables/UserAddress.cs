using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;
using Kaizen.Entities.Addresses;

namespace Kaizen.Entities.LinkTables
{
    [Audited]
    public class UserAddress : Entity<long>
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public long AddressId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; }
    }
}
