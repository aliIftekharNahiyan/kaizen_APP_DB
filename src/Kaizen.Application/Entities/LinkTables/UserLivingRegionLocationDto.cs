using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Authorization.Users;
using Kaizen.Entities.RegionLocations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Entities.LinkTables
{
    [AutoMapTo(typeof(UserLivingRegionLocation))]
    public class UserLivingRegionLocationDto : EntityDto
    {
        public long UserId { get; set; }
        public long RegionLocationId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(RegionLocationId))]
        public virtual RegionLocation RegionLocation { get; set; }
    }
}