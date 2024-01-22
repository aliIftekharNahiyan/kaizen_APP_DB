using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Entities.LinkTables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Entities.CommunicationGroups
{
    [Audited]
    public class CommunicationGroup : Entity<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(512)]
        public string Description { get; set; }

        [Required]
        public bool Deleted { get; set; } = false;

        public List<CommunicationGroupUser> CommunicationGroupUsers { get; set; }
    }
}
