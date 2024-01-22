using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;

namespace Kaizen.Entities.PaymentTerms
{
    public class PaymentTerm : Entity<long>
    {
        [Required]
        public string Name { get; set; }

        public bool Deleted { get; set; } = false; 
    }
}
