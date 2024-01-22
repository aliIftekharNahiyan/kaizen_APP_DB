using Kaizen.Entities.Lookups.Dto;
using System.Collections.Generic;

namespace Kaizen.Web.Models.Lookups
{
    public class LookupListViewModel
    {
        public IReadOnlyList<LookupDto> Entities { get; set; }
    }
}