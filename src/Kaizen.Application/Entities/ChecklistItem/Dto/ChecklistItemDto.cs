
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.Checklists;

namespace Kaizen.Entities.ChecklistItems.Dto
{
    [AutoMapTo(typeof(ChecklistItem))]
    public class ChecklistItemDto : EntityDto<long>
    {
        public string Name { get; set; }

        public bool Deleted { get; set; }

        public List<ChecklistItemOptionDto> Options { get; set; }
    }
}