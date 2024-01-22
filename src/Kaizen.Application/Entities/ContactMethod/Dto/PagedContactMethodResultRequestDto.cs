
            using System;
            using Abp.Application.Services.Dto;

            namespace Kaizen.Entities.ContactMethods.Dto
            {
                public class PagedContactMethodResultRequestDto : PagedResultRequestDto
                {
                    public string Keyword { get; set; }
                    public string Sorting { get; set; }
                }
            }