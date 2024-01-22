
            using System;
            using Abp.Application.Services.Dto;

            namespace Kaizen.Entities.PaymentTerms.Dto
            {
                public class PagedPaymentTermResultRequestDto : PagedResultRequestDto
                {
                    public string Keyword { get; set; }
                    public string Sorting { get; set; }
                }
            }