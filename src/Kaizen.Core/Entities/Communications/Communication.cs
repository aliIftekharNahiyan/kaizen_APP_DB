using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Entities.Communications
{
    [Audited]
    public class Communication : Entity<long>
    {
        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public bool Deleted { get; set; } = false;

        [Required]
        public bool IsDraft { get; set; } = false;

        public DateTime? DateSent { get; set; }
        public DateTime? CompletedDate { get; set; }

        public CommunicationStatus Status { get; set; }
    }
}
