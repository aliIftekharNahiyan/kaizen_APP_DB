using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;
using Kaizen.Entities.Regions;

namespace Kaizen.Entities.RegionLocations
{
    [Audited]
    public class RegionLocation : Entity<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public long RegionId { get; set; }

        [Required]
        public bool Deleted { get; set; } = false;

        [ForeignKey(nameof(RegionId))]
        public virtual Region Region { get; set; }
    }
}
