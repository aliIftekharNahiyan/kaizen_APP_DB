using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;
using Kaizen.Entities.Addresses;
using Kaizen.Entities.CommunicationGroups;
using Kaizen.Entities.Companies;

namespace Kaizen.Entities.LinkTables
{
    [Audited]
    public class CommunicationGroupUser : Entity<long>
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public long CommunicationGroupId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(CommunicationGroupId))]
        public virtual CommunicationGroup CommunicationGroup { get; set; }
    }
}
