
using Kaizen.Entities.Regions.Dto;
using System.Collections.Generic;

namespace Kaizen.Web.Models.Regions
{
    public class RegionListViewModel
    {
        public IReadOnlyList<RegionDto> Entities { get; set; }
    }
}