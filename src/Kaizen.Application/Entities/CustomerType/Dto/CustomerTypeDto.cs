
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Kaizen.Entities.CustomerTypes.Dto
{
    [AutoMapTo(typeof(CustomerType))]
    public class CustomerTypeDto : EntityDto<long>
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
    }
}