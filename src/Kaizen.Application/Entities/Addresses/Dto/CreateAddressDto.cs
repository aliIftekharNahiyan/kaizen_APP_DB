using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Kaizen.Entities.Addresses;

namespace Kaizen.Entities.Addresses.Dto
{
    [AutoMapTo(typeof(Address))]
    public class CreateAddressDto : IShouldNormalize
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

        public long UserId { get; set; }

        public void Normalize()
        {

        }
    }
}
