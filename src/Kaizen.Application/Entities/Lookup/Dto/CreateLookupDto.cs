
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Entities.Lookups.Dto
{
    [AutoMapTo(typeof(Lookup))]
    public class CreateLookupDto : EntityDto<long>
    {

        public long LookTypeId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string SName { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; } = false;

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}