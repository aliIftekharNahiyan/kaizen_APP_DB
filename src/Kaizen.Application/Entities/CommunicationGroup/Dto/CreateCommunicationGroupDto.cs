using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Kaizen.Entities.CommunicationGroups.Dto
{
    [AutoMapTo(typeof(CommunicationGroup))]
    public class CreateCommunicationGroupDto : EntityDto<long>
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Deleted { get; set; }

        public List<int> UserList { get; set; }
    }
}