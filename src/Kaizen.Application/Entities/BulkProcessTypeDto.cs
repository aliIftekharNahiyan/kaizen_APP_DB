using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.Addresses;

namespace Kaizen.Entities.BulkProcesses.Dto
{
    public class BulkProcessTypeDto<T>
        where T : class
    {
        public T BulkProcessItem { get; set; }
    }
}

