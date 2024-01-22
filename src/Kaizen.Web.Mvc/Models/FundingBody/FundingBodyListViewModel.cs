
using System.Collections.Generic;
using Kaizen.Entities.FundingBodys.Dto;

namespace Kaizen.Web.Models.FundingBodys
{
    public class FundingBodyListViewModel
    {
        public IReadOnlyList<FundingBodyDto> Entities { get; set; }
    }
}