
            using System;
            using Abp.Application.Services.Dto;

            namespace Kaizen.Entities.StorageFiles.Dto
            {
                public class PagedStorageFileResultRequestDto : PagedResultRequestDto
                {
                    public string Keyword { get; set; }
                    public string Sorting { get; set; }
                }
            }