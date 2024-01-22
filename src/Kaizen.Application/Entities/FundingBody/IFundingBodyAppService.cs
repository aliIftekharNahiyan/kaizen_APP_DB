
using Abp.Application.Services;
using Kaizen.Entities.FundingBodys.Dto;

namespace Kaizen.Entities.FundingBodys
{
    public interface IFundingBodyAppService : IAsyncCrudAppService<FundingBodyDto, long, PagedFundingBodyResultRequestDto, CreateFundingBodyDto, FundingBodyDto>
    {

    }
}