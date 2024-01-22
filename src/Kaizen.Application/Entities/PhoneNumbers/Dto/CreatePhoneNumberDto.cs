using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Kaizen.Entities.Addresses;

namespace Kaizen.Entities.PhoneNumbers.Dto
{
    [AutoMapTo(typeof(PhoneNumber))]
    public class CreatePhoneNumberDto : IShouldNormalize
    {
        public int CountryCode => 44;

        [Required]
        public int Number { get; set; }

        [Required]
        public PhoneNumberType PhoneNumberType { get; set; }
        public bool IsPrimary { get; set; }

        [Required]
        public long UserId { get; set; }

        public void Normalize()
        {

        }
    }
}
