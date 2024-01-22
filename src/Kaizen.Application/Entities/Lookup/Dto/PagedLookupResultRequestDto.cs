
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Kaizen.Entities.Lookups.Dto
{
    public class PagedLookupResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}