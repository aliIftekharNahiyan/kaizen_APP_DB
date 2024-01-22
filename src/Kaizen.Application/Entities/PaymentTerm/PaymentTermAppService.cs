using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.PaymentTerms.Dto;
using System.Threading.Tasks;
using Abp.Linq.Extensions;

namespace Kaizen.Entities.PaymentTerms
{
    public class PaymentTermAppService : AsyncCrudAppService<PaymentTerm, PaymentTermDto, long, PagedPaymentTermResultRequestDto, CreatePaymentTermDto, PaymentTermDto>, IPaymentTermAppService
    {
        public PaymentTermAppService(
            IRepository<PaymentTerm, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<PaymentTerm> ApplySorting(IQueryable<PaymentTerm> query, PagedPaymentTermResultRequestDto input)
        {
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                return query.OrderBy(input.Sorting);
            }
            else
            {
                return query.OrderBy("name asc");
            }
        }

        protected override IQueryable<PaymentTerm> CreateFilteredQuery(PagedPaymentTermResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(keywordLower));
        }
    }
}