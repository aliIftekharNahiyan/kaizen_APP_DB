using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Extensions;
using Kaizen.Validation;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Entities.Companies.Dto
{
    [AutoMapTo(typeof(Company))]
    public class CreateCompanyDto : EntityDto<long> 
    {

        [Required]
        [MaxLength(256)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [StringLength(512)]
        [MinLength(3)]
        public string Description { get; set; }

        [Required]
        [StringLength(256)]       
        [DataType(DataType.EmailAddress,ErrorMessage = "Invalid email address")]      
        public string SendFromEmailAddress { get; set; }        
        public bool IsActive { get; set; } = true;        
        public bool Deleted { get; set; } = false;        
        public long LogoFileId { get; set; }

        [StringLength(8)]
        [MinLength(3)]
        public string PrimaryBrandColour { get; set; }

        [StringLength(8)]
        [MinLength(3)]
        public string SecondaryBrandColour { get; set; }

        [Display(Name = "Logo")]
        public IFormFile LogoFile { get; set; }


    }
}