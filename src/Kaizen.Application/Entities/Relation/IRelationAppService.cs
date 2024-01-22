using Abp.Application.Services;
using Kaizen.Entities.Lookups.Dto;

namespace Kaizen.Entities.Relations
{
    public interface IRelationAppService : IAsyncCrudAppService<LookupDto, long, PagedLookupResultRequestDto, CreateLookupDto, LookupDto>
    {
        // public IQueryable<Lookup> GetManyQueryable(Func<Lookup, bool> where);
    }
}