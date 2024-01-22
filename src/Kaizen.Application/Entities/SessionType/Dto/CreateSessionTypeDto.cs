
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Kaizen.Entities.SessionTypes.Dto
{
    [AutoMapTo(typeof(SessionType))]
    public class CreateSessionTypeDto : EntityDto<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(512)]
        public string Description { get; set; }
    }
}