using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;

namespace Kaizen.Entities.Addresses
{
    [Audited]
    public class Address : Entity<long>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string Line1 { get; set; }

        [StringLength(256)]
        public string Line2 { get; set; }

        [StringLength(256)]
        public string Line3 { get; set; }

        [Required]
        [StringLength(256)]
        public string City { get; set; }

        [Required]
        [StringLength(256)]
        public string County { get; set; }

        [Required]
        [StringLength(32)]
        public string Postcode { get; set; }

        [Required]
        public bool IsPrimary { get; set; }

        [Required]
        public bool Deleted { get; set; } = false;
    }
}
