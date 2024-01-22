using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Runtime.Validation;

namespace Kaizen.Entities.SupportTypes.Dto
{
    [AutoMapTo(typeof(SupportType))]
    public class CreateSupportTypeDto : IShouldNormalize
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public int Margin { get; set; }

        public void Normalize()
        {

        }
    }
}
