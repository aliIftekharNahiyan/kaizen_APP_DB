
            using System;
            using Abp.Application.Services.Dto;

            namespace Kaizen.Entities.Disabilitys.Dto
            {
                public class PagedDisabilityResultRequestDto : PagedResultRequestDto
                {
                    public string Keyword { get; set; }
                    public string Sorting { get; set; }
                }
            }