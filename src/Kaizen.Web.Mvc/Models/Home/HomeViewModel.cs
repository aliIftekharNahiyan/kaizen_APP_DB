using Abp.Auditing;
using Kaizen.Entities.SupportSessions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Web.Models.Home
{
    public class HomeViewModel
    {
        public List<SupportSession> RecentSupportSessions { get; set; }
    }
}
