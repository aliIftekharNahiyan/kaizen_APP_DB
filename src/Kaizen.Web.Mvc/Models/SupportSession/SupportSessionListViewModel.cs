
using System.Collections.Generic;
using Kaizen.Entities.SupportSessions;
using Kaizen.Entities.SupportSessions.Dto;

namespace Kaizen.Web.Models.SupportSessions
{
    public class SupportSessionListViewModel
    {
        public IReadOnlyList<SupportSessionDto> Entities { get; set; }
    }
}