using Kaizen.Authorization.Users;
using Kaizen.Entities.Lookups;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Web.Models.Users
{
    public class UserKinModalViewModel
    {
        public long UserId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }
        public long? RelationId { get; set; }

        public long? ContactTypeId { get; set; }

        [MaxLength(10)]
        public string Contact { get; set; }

        public bool IsActive { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(ContactTypeId))]
        public virtual Lookup ContactType { get; set; }

        [ForeignKey(nameof(RelationId))]
        public virtual Lookup Relation { get; set; }
    }
}