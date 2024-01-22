
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Kaizen.Entities.Universitys.Dto
{
    [AutoMapTo(typeof(University))]
    public class UniversityDto : EntityDto<long>
    {
        [Required]
        [StringLength(512)]
        public string Name { get; set; }
    }
}