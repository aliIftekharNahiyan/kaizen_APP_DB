
using Kaizen.Entities.PaymentTerms.Dto;
using System.Collections.Generic;

namespace Kaizen.Web.Models.PaymentTerms
{
    public class PaymentTermListViewModel
    {
        public IReadOnlyList<PaymentTermDto> Entities { get; set; }
    }
}