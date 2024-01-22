
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Kaizen.Entities.Regions.Dto
{
    [AutoMapTo(typeof(Region))]
    public class CreateRegionDto : EntityDto<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

    }
}