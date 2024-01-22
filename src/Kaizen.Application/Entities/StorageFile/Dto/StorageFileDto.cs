
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Kaizen.Entities.StorageFiles.Dto
{
    [AutoMapTo(typeof(StorageFile))]
    public class StorageFileDto : EntityDto<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(1024)]
        public string FileUrl { get; set; }

        [Required]
        [StringLength(256)]
        public string FileName { get; set; }

        [Required]
        [StringLength(128)]
        public string MimeType { get; set; }
    }
}