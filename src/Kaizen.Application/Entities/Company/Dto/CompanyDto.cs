using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Entities.Companies.Dto
{
    [AutoMapTo(typeof(Company))]
    public class CompanyDto : EntityDto<long>
    {        
        public string Name { get; set; }     
        public string Description { get; set; }
        public string SendFromEmailAddress { get; set; }
        public bool IsActive { get; set; } = true;
        public bool Deleted { get; set; } = false;
        public long LogoFileId { get; set; }
        public string PrimaryBrandColour { get; set; }
        public string SecondaryBrandColour { get; set; }
        public IFormFile LogoFile { get; set; }

    }
}