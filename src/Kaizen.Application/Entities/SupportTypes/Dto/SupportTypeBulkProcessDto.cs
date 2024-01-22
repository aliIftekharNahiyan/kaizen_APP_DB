using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AutoMapper.Configuration.Annotations;
using Kaizen.Entities.Addresses;

namespace Kaizen.Entities.SupportTypes.Dto
{
    public class SupportTypeBulkProcessDto
    {
        [Required]
        [StringLength(256)]
        [Ignore]
        public string Name { get; set; }

        [Ignore]
        public string Description { get; set; }

        [Required]
        [Ignore]
        public double Cost { get; set; }

        [Required]
        [Ignore]
        public int Margin { get; set; }
    }
}

