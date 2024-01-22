
            using System;
            using Abp.Application.Services.Dto;

            namespace Kaizen.Entities.RegionLocations.Dto
            {
                public class PagedRegionLocationResultRequestDto : PagedResultRequestDto
                {
                    public string Keyword { get; set; }
                    public string Sorting { get; set; }
                }
            }