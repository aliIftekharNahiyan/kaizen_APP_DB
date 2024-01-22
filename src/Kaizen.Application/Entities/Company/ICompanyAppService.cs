using Abp.Application.Services;
using Kaizen.Entities.Companies.Dto;

namespace Kaizen.Entities.Companies
{
    public interface ICompanyAppService : IAsyncCrudAppService<CompanyDto, long, PagedCompanyResultRequestDto, CreateCompanyDto, CompanyDto>
    {

    }
}