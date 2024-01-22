
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.AcademicTerms.Dto
{
    public class PagedAcademicTermRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}