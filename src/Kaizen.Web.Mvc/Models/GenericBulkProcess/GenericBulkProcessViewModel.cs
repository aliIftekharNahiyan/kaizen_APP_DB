using Kaizen.Enums;
using Microsoft.AspNetCore.Http;
using System;

namespace Kaizen.Web.Models.GenericBulkProcess
{
    [Serializable]
    public class GenericBulkProcessViewModel
    {
        public long TableId { get; set; }
        public IFormFile UploadedFile { get; set; }
        public BulkProcessType BulkProcessType { get; set; }
        public string ElementId { get; set; }
    }
}
