using Kaizen.Entities.BulkProcesses.Dto;
using Kaizen.Enums;
using System;
using System.Collections.Generic;

namespace Kaizen.Web.Models.BulkProcess
{
    [Serializable]
    public class BulkProcessJobArgs<T>
        where T : class
    {
        public long TableId { get; set; }
        public long CreatedByUserId { get; set; }
        public List<BulkProcessTypeDto<T>> FileResults { get; set; }
        public BulkProcessType BulkProcessType { get; set; }
    }
}
