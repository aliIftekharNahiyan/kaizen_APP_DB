using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;
using Kaizen.Entities.Skills;

namespace Kaizen.Entities.LinkTables
{
    [Audited]
    public class UserSkill : Entity<long>
    {       
        public long SkillId { get; set; }

        public long UserId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(SkillId))]
        public virtual Skill Skill { get; set; }
    }
}