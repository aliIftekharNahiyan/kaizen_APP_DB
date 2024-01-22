
using Kaizen.Entities.CustomerTypes.Dto;
using System.Collections.Generic;

namespace Kaizen.Web.Models.CustomerTypes
{
    public class CustomerTypeListViewModel
    {
        public IReadOnlyList<CustomerTypeDto> Entities { get; set; }
    }
}