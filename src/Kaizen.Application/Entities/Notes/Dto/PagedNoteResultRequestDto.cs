using Abp.Application.Services.Dto;
using System;

namespace Kaizen.Entities.Notes.Dto
{
    //custom PagedResultRequestDto
    public class PagedNoteResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
        public string EntityType { get; set; }
        public long EntityId { get; set; }
    }
}
