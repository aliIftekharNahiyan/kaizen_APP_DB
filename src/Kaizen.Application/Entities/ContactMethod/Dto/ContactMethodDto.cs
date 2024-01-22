
            using System;
            using System.ComponentModel.DataAnnotations;
            using Abp.Application.Services.Dto;
            using Abp.AutoMapper;

            namespace Kaizen.Entities.ContactMethods.Dto
            {
                [AutoMapTo(typeof(ContactMethod))]
                public class ContactMethodDto : EntityDto<long>
                {
                    [Required]
                    [StringLength(128)]
                    public string Name { get; set; }
                }
            }