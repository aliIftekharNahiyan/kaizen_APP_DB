using Kaizen.Entities.RegionLocations.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Kaizen.Web.Models.RegionLocations
{
    public class RegionLocationEditViewModel
    {
        public RegionLocationDto RegionLocation { get; set; }
        public List<SelectListItem> RegionList { get; set; }
    }
}