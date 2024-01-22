
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.AcademicYears.Dto
{
    public class PagedAcademicYearRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}