
            using Abp.Application.Services;
            using Kaizen.Entities.Disabilitys.Dto;

            namespace Kaizen.Entities.Disabilitys
            {
                public interface IDisabilityAppService : IAsyncCrudAppService<DisabilityDto, long, PagedDisabilityResultRequestDto, CreateDisabilityDto, DisabilityDto>
                {

                }
            }