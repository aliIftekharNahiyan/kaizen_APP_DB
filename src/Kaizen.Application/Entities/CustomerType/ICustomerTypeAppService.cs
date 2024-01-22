
            using Abp.Application.Services;
            using Kaizen.Entities.CustomerTypes.Dto;

            namespace Kaizen.Entities.CustomerTypes
            {
                public interface ICustomerTypeAppService : IAsyncCrudAppService<CustomerTypeDto, long, PagedCustomerTypeResultRequestDto, CreateCustomerTypeDto, CustomerTypeDto>
                {

                }
            }