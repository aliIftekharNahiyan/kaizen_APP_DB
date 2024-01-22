using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;

namespace Kaizen.Entities.Notes
{
    public class Note : Entity<long>
    {
        [Required]
        public long CreatedBy { get; set; }

        public string Content { get; set; }

        public long EntityId { get; set; }
        public string EntityType { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public int NoteType { get; set; }

        public bool Deleted { get; set; }

        public bool IsImportant { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public User CreatedByUser { get; set; }
    }

    public enum NoteType
    {
        Public = 0,
        Internal = 1
    }
}
