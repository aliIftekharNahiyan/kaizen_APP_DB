
            using System;
            using System.ComponentModel.DataAnnotations;
            using Abp.Application.Services.Dto;
            using Abp.AutoMapper;

            namespace Kaizen.Entities.PaymentTerms.Dto
            {
                [AutoMapTo(typeof(PaymentTerm))]
                public class CreatePaymentTermDto : EntityDto<long>
                {
        [Required]
        public string Name { get; set; }

    }
}