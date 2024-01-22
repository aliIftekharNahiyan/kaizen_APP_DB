using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.Regions.Dto;

namespace Kaizen.Entities.RegionLocations.Dto
{
    [AutoMapTo(typeof(RegionLocation))]
    public class RegionLocationDto : EntityDto<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public long RegionId { get; set; }

        public virtual RegionDto Region { get; set; }
    }
}