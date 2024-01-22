using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Authorization.Users;
using Kaizen.Entities.LinkTables;
using Kaizen.Users.Dto;

namespace Kaizen.Entities.CommunicationGroups.Dto
{
    [AutoMapTo(typeof(CommunicationGroupUser))]
    public class CommunicationGroupUserDto : EntityDto<long>
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public long CommunicationGroupId { get; set; }

        public virtual UserDto User { get; set; }

        public virtual CommunicationGroupDto CommunicationGroup { get; set; }
    }
}