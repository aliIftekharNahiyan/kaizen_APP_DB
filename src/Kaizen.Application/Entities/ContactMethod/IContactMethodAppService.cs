
            using Abp.Application.Services;
            using Kaizen.Entities.ContactMethods.Dto;

            namespace Kaizen.Entities.ContactMethods
            {
                public interface IContactMethodAppService : IAsyncCrudAppService<ContactMethodDto, long, PagedContactMethodResultRequestDto, CreateContactMethodDto, ContactMethodDto>
                {

                }
            }