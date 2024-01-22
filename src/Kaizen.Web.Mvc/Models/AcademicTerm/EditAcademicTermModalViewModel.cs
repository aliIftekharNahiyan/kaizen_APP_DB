
using System.Collections.Generic;
using Kaizen.Entities.AcademicTerms.Dto;
using Kaizen.Entities.AcademicYears.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaizen.Web.Models.AcademicTerm
{
    public class EditAcademicTermModalViewModel
    {
        public AcademicTermDto AcademicTerm { get; set; }
    }
}