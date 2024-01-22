
using Kaizen.Entities.SessionTypes.Dto;
using System.Collections.Generic;

namespace Kaizen.Web.Models.SessionTypes
{
    public class SessionTypeListViewModel
    {
        public IReadOnlyList<SessionTypeDto> Entities { get; set; }
    }
}