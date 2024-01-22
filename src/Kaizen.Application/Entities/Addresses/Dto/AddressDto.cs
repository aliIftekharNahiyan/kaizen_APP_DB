using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.Addresses;

namespace Kaizen.Entities.Addresses.Dto
{
    [AutoMapFrom(typeof(Address))]
    public class AddressDto : EntityDto<long>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string County { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        public bool IsPrimary { get; set; }
    }
}

