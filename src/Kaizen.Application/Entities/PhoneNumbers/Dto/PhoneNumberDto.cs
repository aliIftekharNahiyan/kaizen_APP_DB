using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.Addresses;

namespace Kaizen.Entities.PhoneNumbers.Dto
{
    [AutoMapFrom(typeof(PhoneNumber))]
    public class PhoneNumberDto : EntityDto<long>
    {
        public int CountryCode => 44;

        [Required]
        public int Number { get; set; }

        [Required]
        public PhoneNumberType PhoneNumberType { get; set; }

        public bool IsPrimary { get; set; }

        [Required]
        public long UserId { get; set; }
    }
}
