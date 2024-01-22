
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Kaizen.Entities.FundingBodys.Dto
{
    [AutoMapTo(typeof(FundingBody))]
    public class FundingBodyDto : EntityDto<long>
    {
        public string Name { get; set; }
    }
}