using Kaizen.Entities.BulkProcesses.Dto;
using Kaizen.Entities.GenericBulkProcesses.Dto;
using Kaizen.Enums;
using System;
using System.Collections.Generic;

namespace Kaizen.Web.Models.GenericBulkProcess
{
    [Serializable]
    public class GenericBulkProcessJobArgs<T>
        where T : class
    {
        public long TableId { get; set; }
        public long CreatedByUserId { get; set; }
        public List<GenericBulkProcessTypeDto<T>> FileResults { get; set; }
        public BulkProcessType BulkProcessType { get; set; }
    }
}
