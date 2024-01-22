
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.Checklists;

namespace Kaizen.Entities.ChecklistItems.Dto
{
    [AutoMapTo(typeof(ChecklistItem))]
    public class CreateChecklistItemDto : EntityDto<long>
    {
        [Required]
        [StringLength(1024)]
        public string Name { get; set; }

        public bool Deleted { get; set; }

        public virtual List<ChecklistItemOption> Options { get; set; }
    }
}