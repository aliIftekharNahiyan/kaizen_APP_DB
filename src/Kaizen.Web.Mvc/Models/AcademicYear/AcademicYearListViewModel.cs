
using System.Collections.Generic;
using Kaizen.Entities.AcademicYears.Dto;

namespace Kaizen.Web.Models.AcademicYear
{
    public class AcademicYearListViewModel
    {
        public IReadOnlyList<AcademicYearDto> Entities { get; set; }
    }
}