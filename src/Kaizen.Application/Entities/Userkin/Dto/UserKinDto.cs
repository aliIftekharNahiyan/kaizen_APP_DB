using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Authorization.Users;
using Kaizen.Entities.Lookups;
using Kaizen.Entities.Lookups.Dto;

namespace Kaizen.Entities.UserKins.Dto
{
    [AutoMapFrom(typeof(UserKin))]
    public class UserKinDto : EntityDto<long>
    {
        public long UserId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        public long? RelationId { get; set; }

        public long? ContactTypeId { get; set; }

        [MaxLength(10)]
        public string Contact { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; } = false;

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }


        [ForeignKey(nameof(UserId))]
        public  User User { get; set; }

        [ForeignKey(nameof(ContactTypeId))]
        public virtual LookupDto ContactType { get; set; }

        [ForeignKey(nameof(RelationId))]
        public virtual LookupDto Relation { get; set; }
    }
}