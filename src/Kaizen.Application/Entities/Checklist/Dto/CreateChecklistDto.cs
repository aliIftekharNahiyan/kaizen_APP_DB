
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.ChecklistItems.Dto;

namespace Kaizen.Entities.Checklists.Dto
{
    [AutoMapTo(typeof(Checklist))]
    public class CreateChecklistDto : EntityDto<long>
    {
        [Required]
        [StringLength(512)]
        public string Name { get; set; }

        public virtual List<ChecklistCheckListItemDto> CheckListItems { get; set; }
    }
}