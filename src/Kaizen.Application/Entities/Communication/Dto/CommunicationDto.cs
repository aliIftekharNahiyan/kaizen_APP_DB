
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.AcademicYears;
using Kaizen.Entities.AcademicYears.Dto;
using Kaizen.Entities.Companies.Dto;
using Kaizen.Entities.SupportTypes.Dto;

namespace Kaizen.Entities.Communications.Dto
{
    [AutoMapTo(typeof(Communication))]
    public class CommunicationDto : EntityDto<long>
    {
        [Required]
        public DateTime CreatedDate { get; set; }

        public bool Deleted { get; set; }
    }
}