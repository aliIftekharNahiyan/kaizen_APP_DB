using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Authorization.Users;
using Kaizen.Entities.RegionLocations;

namespace Kaizen.Entities.LinkTables
{
    [AutoMapTo(typeof(UserWorkRegionLocation))]
    public class UserWorkRegionLocationDto : EntityDto<long>
    {
        public long UserId { get; set; }
        public long RegionLocationId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(RegionLocationId))]
        public virtual RegionLocation RegionLocation { get; set; }
    }
}