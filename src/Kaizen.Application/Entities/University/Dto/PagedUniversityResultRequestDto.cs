
using System;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.Universitys.Dto
{
    public class PagedUniversityResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}