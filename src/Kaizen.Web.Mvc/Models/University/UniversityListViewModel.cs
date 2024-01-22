
using Kaizen.Entities.Universitys.Dto;
using System.Collections.Generic;

namespace Kaizen.Web.Models.Universitys
{
    public class UniversityListViewModel
    {
        public IReadOnlyList<UniversityDto> Entities { get; set; }
    }
}