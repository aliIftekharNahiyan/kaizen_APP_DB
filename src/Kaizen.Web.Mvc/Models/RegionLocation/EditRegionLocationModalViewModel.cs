using Kaizen.Entities.RegionLocations.Dto;
using Kaizen.Entities.Regions.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Kaizen.Web.Models.RegionLocations
{
    public class EditRegionLocationModalViewModel
    {
        public RegionLocationDto RegionLocation { get; set; }
        public List<SelectListItem> RegionList { get; set; }
    }
}