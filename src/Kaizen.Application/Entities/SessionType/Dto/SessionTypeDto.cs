
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.SessionTypes;

namespace Kaizen.Entities.SessionTypes.Dto
{
    [AutoMapTo(typeof(SessionType))]
    public class SessionTypeDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        //public bool Deleted { get; set; } = false;
    }
}