
using System.Collections.Generic;
using Kaizen.Entities.CommunicationGroups.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaizen.Web.Models.CommunicationGroup
{
    public class CommunicationGroupListViewModel
    {
        public IReadOnlyList<CommunicationGroupDto> Entities { get; set; }
        public List<SelectListItem> Users { get; set; }
    }
}