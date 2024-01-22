
using System.Collections.Generic;
using Kaizen.Entities.AcademicTerms.Dto;
using Kaizen.Entities.AcademicYears.Dto;
using Kaizen.Entities.InfoCheckLists.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaizen.Web.Models.InfoCheckList
{
    public class EditInfoCheckListModalViewModel
    {
        public InfoCheckListDto InfoCheckList { get; set; }
    }
}