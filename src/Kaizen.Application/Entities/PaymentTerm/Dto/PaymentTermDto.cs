
            using System;
            using System.ComponentModel.DataAnnotations;
            using Abp.Application.Services.Dto;
            using Abp.AutoMapper;

            namespace Kaizen.Entities.PaymentTerms.Dto
            {
                [AutoMapTo(typeof(PaymentTerm))]
                public class PaymentTermDto : EntityDto<long>
                {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
    }
            }