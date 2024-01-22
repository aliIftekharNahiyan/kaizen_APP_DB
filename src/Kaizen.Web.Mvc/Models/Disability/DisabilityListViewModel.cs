
using Kaizen.Entities.Disabilitys.Dto;
using System.Collections.Generic;

namespace Kaizen.Web.Models.Disabilitys
{
    public class DisabilityListViewModel
    {
        public IReadOnlyList<DisabilityDto> Entities { get; set; }
    }
}