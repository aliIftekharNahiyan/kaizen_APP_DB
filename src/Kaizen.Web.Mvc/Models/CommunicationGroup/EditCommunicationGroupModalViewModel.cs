using Kaizen.Entities.CommunicationGroups.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Kaizen.Web.Models.CommunicationGroup
{
    public class EditCommunicationGroupModalViewModel
    {
        public CommunicationGroupDto CommunicationGroup { get; set; }
        public List<SelectListItem> Users { get; set; }
    }
}