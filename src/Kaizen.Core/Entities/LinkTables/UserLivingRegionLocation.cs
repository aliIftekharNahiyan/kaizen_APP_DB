using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;
using Kaizen.Entities.RegionLocations;

namespace Kaizen.Entities.LinkTables
{
    [Audited]
    public class UserLivingRegionLocation : Entity<long>
    {
        public long UserId { get; set; }
        public long RegionLocationId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(RegionLocationId))]
        public virtual RegionLocation RegionLocation { get; set; }
    }
}