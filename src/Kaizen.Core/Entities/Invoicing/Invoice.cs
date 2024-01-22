using System;
using Abp.Auditing;
using Abp.Domain.Entities;

namespace Kaizen.Entities.Invoicing
{
    [Audited]
    public class Invoice : Entity<long>
    {
        public DateTime CreatedDate { get; set; }
    }
}
