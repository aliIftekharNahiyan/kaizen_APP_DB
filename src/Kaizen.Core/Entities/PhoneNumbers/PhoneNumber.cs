using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;

namespace Kaizen.Entities.PhoneNumbers
{
    public class PhoneNumber : Entity<long>
    {
        public int CountryCode => 44;

        [Required]
        public int Number { get; set; }

        [Required]
        public PhoneNumberType PhoneNumberType { get; set; }
        public bool IsPrimary { get; set; }

        [Required]
        public long UserId { get; set; }

        public virtual User User { get; set; }
    }

    public enum PhoneNumberType
    {
        Mobile,
        Telephone
    }
}
