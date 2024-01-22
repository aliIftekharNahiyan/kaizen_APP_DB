using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.Addresses;
using Kaizen.Entities.Checklists.Dto;

namespace Kaizen.Entities.SupportTypes.Dto
{
    [AutoMapFrom(typeof(SupportType))]
    public class SupportTypeDto : EntityDto<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public long? CheckListId { get; set; } 

        public string Description { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public int Margin { get; set; }

        public double SellPrice { get; set; }

        public bool Deleted { get; set; }

        public virtual ChecklistDto Checklist { get; set; }
    }
}

