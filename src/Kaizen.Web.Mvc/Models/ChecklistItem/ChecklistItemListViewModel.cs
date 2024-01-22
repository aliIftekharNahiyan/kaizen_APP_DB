
            using System.Collections.Generic;
            using Kaizen.Entities.ChecklistItems.Dto;

            namespace Kaizen.Web.Models.ChecklistItems
            {
                public class ChecklistItemListViewModel
                {
                    public IReadOnlyList<ChecklistItemDto> Entities { get; set; }
                }
            }