
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using static Kaizen.Enums.CommonEnum;

namespace Kaizen.Entities.Lookups.Dto
{
    [AutoMapTo(typeof(Lookup))]
    public class LookupDto : EntityDto<long>
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

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string LookupType
        {
            get { return EnumHelper.GetEnumDescription<CommonEnum.LookupType>((CommonEnum.LookupType)LookTypeId).ToString(); }
        }
    }
}