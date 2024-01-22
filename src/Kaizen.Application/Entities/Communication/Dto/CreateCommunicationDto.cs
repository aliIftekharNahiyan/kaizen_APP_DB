
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.Communications;

namespace Kaizen.Entities.Communications.Dto
{
    [AutoMapTo(typeof(Communication))]
    public class CreateCommunicationDto : EntityDto<long>
    {
        [Required]
        public DateTime CreatedDate { get; set; }

        public bool Deleted { get; set; }
    }
}