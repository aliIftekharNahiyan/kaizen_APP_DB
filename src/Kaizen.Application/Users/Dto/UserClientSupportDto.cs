using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Authorization.Users;
using Kaizen.Entities.LinkTables;
using Kaizen.Entities.Skills;
using Kaizen.Entities.UserClientSupports;

namespace Kaizen.Users.Dto
{
    [AutoMapFrom(typeof(UserClientSupport))]
    public class UserClientSupportDto : EntityDto<long>
    {
        public long UserId { get; set; }

        public long ClientId { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }


        [ForeignKey(nameof(UserId))]
        public virtual User Users { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual User Clients { get; set; }
      

    }
}
