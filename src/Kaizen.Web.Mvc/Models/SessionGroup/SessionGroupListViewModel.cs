
using System.Collections.Generic;
using Kaizen.Entities.SessionGroups.Dto;

namespace Kaizen.Web.Models.SessionGroups
{
    public class SessionGroupListViewModel
    {
        public IReadOnlyList<SessionGroupDto> Entities { get; set; }
    }
}