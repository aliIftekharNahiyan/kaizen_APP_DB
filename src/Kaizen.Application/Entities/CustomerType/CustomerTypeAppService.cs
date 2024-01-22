
using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.CustomerTypes.Dto;

namespace Kaizen.Entities.CustomerTypes
            {
                public class CustomerTypeAppService : AsyncCrudAppService<CustomerType, CustomerTypeDto, long, PagedCustomerTypeResultRequestDto, CreateCustomerTypeDto, CustomerTypeDto>, ICustomerTypeAppService
                {
                    public CustomerTypeAppService(
                        IRepository<CustomerType, long> repository)
                        : base(repository)
                    {
                    }

                    protected override IQueryable<CustomerType> ApplySorting(IQueryable<CustomerType> query, PagedCustomerTypeResultRequestDto input)
                    {
                        if (!input.Sorting.IsNullOrWhiteSpace())
                        {
                            return query.OrderBy(input.Sorting);
                        }

                        return base.ApplySorting(query, input);
                    }

                    protected override IQueryable<CustomerType> CreateFilteredQuery(PagedCustomerTypeResultRequestDto input)
                    {
                        var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

                        return Repository.GetAllIncluding();
                    }
                }
            }