
            using AutoMapper;
            using Kaizen.Entities.PaymentTerms;
            using Kaizen.Entities.PaymentTerms.Dto;

            namespace Kaizen.Users.Dto
            {
                public class PaymentTermMapProfile : Profile
                {
                    public PaymentTermMapProfile()
                    {
                        CreateMap<PaymentTerm, PaymentTermDto>();
                        CreateMap<PaymentTerm, CreatePaymentTermDto>();

                        CreateMap<CreatePaymentTermDto, PaymentTerm>();
                    }
                }
            }