using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Extensions;
using Kaizen.Entities.BulkProcess;
using Kaizen.Enums;
using Kaizen.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Entities.BulkProcessJobs.Dto
{
    [AutoMapTo(typeof(BulkProcessJob))]
    public class CreateBulkProcessJobDto : EntityDto<long> 
    {
        public BulkProcessType BulkProcessType { get; set; }
        public BulkProcessStatus BulkProcessStatus { get; set; }

        public long CreatedByUserId { get; set; }

        public int TotalRecords { get; set; }
        public int SuccessfulRecords { get; set; }
        public int FailedRecords { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime? DateUpdated { get; set; }

        public bool? Cancelled { get; set; }

        public DateTime? DateCancelled { get; set; }

        [Required]
        [StringLength(1024)]
        public string FileUploadedUrl { get; set; }

        [StringLength(1024)]
        public string FileResultUrl { get; set; }


    }
}