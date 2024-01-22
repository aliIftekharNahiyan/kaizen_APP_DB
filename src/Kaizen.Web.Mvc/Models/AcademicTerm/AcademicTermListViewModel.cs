
using System.Collections.Generic;
using Kaizen.Entities.AcademicTerms.Dto;

namespace Kaizen.Web.Models.AcademicTerm
{
    public class AcademicTermListViewModel
    {
        public IReadOnlyList<AcademicTermDto> Entities { get; set; }
    }
}