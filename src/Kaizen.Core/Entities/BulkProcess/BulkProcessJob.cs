using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.ObjectComparators.IntComparators;
using Kaizen.Authorization.Users;
using Kaizen.Enums;

namespace Kaizen.Entities.BulkProcess
{
    public class BulkProcessJob : Entity<long>
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
