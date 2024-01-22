using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.ContactMethods.Dto;

namespace Kaizen.Entities.ContactMethods
{
    public class ContactMethodAppService : AsyncCrudAppService<ContactMethod, ContactMethodDto, long, PagedContactMethodResultRequestDto, CreateContactMethodDto, ContactMethodDto>, IContactMethodAppService
                {
                    public ContactMethodAppService(
                        IRepository<ContactMethod, long> repository)
                        : base(repository)
                    {
                    }

                    protected override IQueryable<ContactMethod> ApplySorting(IQueryable<ContactMethod> query, PagedContactMethodResultRequestDto input)
                    {
                        if (!input.Sorting.IsNullOrWhiteSpace())
                        {
                            return query.OrderBy(input.Sorting);
                        }

                        return base.ApplySorting(query, input);
                    }

                    protected override IQueryable<ContactMethod> CreateFilteredQuery(PagedContactMethodResultRequestDto input)
                    {
                        var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

                        return Repository.GetAllIncluding();
                    }
                }
            }