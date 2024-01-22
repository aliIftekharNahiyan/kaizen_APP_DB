using System;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;

namespace Kaizen.Entities.LinkTables
{
    [Audited]
    public class UserDisability : Entity<long>
    {
        public long UserId { get; set; }
        public long DisabilityId { get; set; }
    }
}
