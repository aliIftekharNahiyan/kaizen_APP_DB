
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.ChecklistItems.Dto;
using Kaizen.Entities.LinkTables;

namespace Kaizen.Entities.Checklists.Dto
{
    [AutoMapTo(typeof(Checklist))]
    public class ChecklistDto : EntityDto<long>
    {
        public string Name { get; set; }

        public virtual List<ChecklistCheckListItemDto> CheckListItems { get; set; }
    }
}