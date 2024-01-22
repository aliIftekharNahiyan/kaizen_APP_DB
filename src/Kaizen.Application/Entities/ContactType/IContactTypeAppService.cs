using Abp.Application.Services;
using Kaizen.Entities.Lookups.Dto;

namespace Kaizen.Entities.ContactTypes
{
    public interface IContactTypeAppService : IAsyncCrudAppService<LookupDto, long, PagedLookupResultRequestDto, CreateLookupDto, LookupDto>
    {
        // public IQueryable<Lookup> GetManyQueryable(Func<Lookup, bool> where);
    }
}