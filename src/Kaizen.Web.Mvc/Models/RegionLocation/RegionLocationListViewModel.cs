
using Kaizen.Entities.RegionLocations.Dto;
using Kaizen.Entities.Regions.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Kaizen.Web.Models.RegionLocations
{
    public class RegionLocationListViewModel
    {
        public IReadOnlyList<RegionLocationDto> Entities { get; set; }
    }
}