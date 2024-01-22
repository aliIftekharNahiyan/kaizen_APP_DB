using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;

namespace Kaizen.Entities.CustomerTypes
{
    [Audited]
    public class CustomerType : Entity<long>
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public bool Deleted { get; set; } = false;
    }
}
