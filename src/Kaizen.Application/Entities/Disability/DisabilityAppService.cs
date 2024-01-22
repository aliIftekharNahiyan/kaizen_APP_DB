using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.Disabilitys.Dto;

namespace Kaizen.Entities.Disabilitys
            {
                public class DisabilityAppService : AsyncCrudAppService<Disability, DisabilityDto, long, PagedDisabilityResultRequestDto, CreateDisabilityDto, DisabilityDto>, IDisabilityAppService
                {
                    public DisabilityAppService(
                        IRepository<Disability, long> repository)
                        : base(repository)
                    {
                    }

                    protected override IQueryable<Disability> ApplySorting(IQueryable<Disability> query, PagedDisabilityResultRequestDto input)
                    {
                        if (!input.Sorting.IsNullOrWhiteSpace())
                        {
                            return query.OrderBy(input.Sorting);
                        }

                        return base.ApplySorting(query, input);
                    }

                    protected override IQueryable<Disability> CreateFilteredQuery(PagedDisabilityResultRequestDto input)
                    {
                        var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

                        return Repository.GetAllIncluding();
                    }
                }
            }