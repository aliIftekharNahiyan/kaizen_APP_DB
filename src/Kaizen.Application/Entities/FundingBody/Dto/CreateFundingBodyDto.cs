
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Kaizen.Entities.FundingBodys.Dto
{
    [AutoMapTo(typeof(FundingBody))]
    public class CreateFundingBodyDto : EntityDto<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
    }
}