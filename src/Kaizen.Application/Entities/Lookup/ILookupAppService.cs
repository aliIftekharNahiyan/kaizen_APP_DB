
using Abp.Application.Services;
using Kaizen.Entities.Lookups.Dto;
using System.Linq;
using System;

namespace Kaizen.Entities.Lookups
{
    public interface ILookupAppService : IAsyncCrudAppService<LookupDto, long, PagedLookupResultRequestDto, CreateLookupDto, LookupDto>
    {
       // public IQueryable<Lookup> GetManyQueryable(Func<Lookup, bool> where);
    }
}