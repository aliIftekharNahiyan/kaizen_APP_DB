
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Kaizen.Entities.RegionLocations.Dto
{
    [AutoMapTo(typeof(RegionLocation))]
    public class CreateRegionLocationDto : EntityDto<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public long RegionId { get; set; }
    }
}