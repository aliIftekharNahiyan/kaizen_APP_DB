
            using Abp.Application.Services;
            using Kaizen.Entities.PaymentTerms.Dto;

            namespace Kaizen.Entities.PaymentTerms
            {
                public interface IPaymentTermAppService : IAsyncCrudAppService<PaymentTermDto, long, PagedPaymentTermResultRequestDto, CreatePaymentTermDto, PaymentTermDto>
                {

                }
            }