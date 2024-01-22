using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.LinkTables;

namespace Kaizen.Entities.CommunicationGroups.Dto
{
    [AutoMapTo(typeof(CommunicationGroup))]
    public class CommunicationGroupDto : EntityDto<long>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool Deleted { get; set; }

        public List<long> UserList { get; set; }

        public List<CommunicationGroupUserDto> CommunicationGroupUsers { get; set; }
    }
}