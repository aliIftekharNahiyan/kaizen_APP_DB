
using System.Collections.Generic;
using Kaizen.Entities.Checklists.Dto;

namespace Kaizen.Web.Models.Checklists
{
    public class ChecklistListViewModel
    {
        public IReadOnlyList<ChecklistDto> Entities { get; set; }
    }
}