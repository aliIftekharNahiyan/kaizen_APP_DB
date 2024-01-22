
using System.Collections.Generic;
using Kaizen.Entities.AcademicTerms.Dto;
using Kaizen.Entities.InfoCheckLists.Dto;

namespace Kaizen.Web.Models.InfoCheckList
{
    public class InfoCheckListListViewModel
    {
        public IReadOnlyList<InfoCheckListDto> Entities { get; set; }
    }
}