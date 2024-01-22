using Abp.Application.Services.Dto;
using System;

namespace Kaizen.Entities.History.Dto
{
    public class PagedHistoryResultRequestDto : PagedResultRequestDto
    {
       public string EntityType { get; set; }
       public string EntityId { get; set; }
       public int ChangesetId { get; set; }
    }
}

