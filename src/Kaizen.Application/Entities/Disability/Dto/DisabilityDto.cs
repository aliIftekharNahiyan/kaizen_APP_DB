
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Kaizen.Entities.Disabilitys.Dto
{
    [AutoMapTo(typeof(Disability))]
    public class DisabilityDto : EntityDto<long>
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
    }
}