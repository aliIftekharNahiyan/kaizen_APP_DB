
            using AutoMapper;
            using Kaizen.Entities.ContactMethods;
            using Kaizen.Entities.ContactMethods.Dto;

            namespace Kaizen.Users.Dto
            {
                public class ContactMethodMapProfile : Profile
                {
                    public ContactMethodMapProfile()
                    {
                        CreateMap<ContactMethod, ContactMethodDto>();
                        CreateMap<ContactMethod, CreateContactMethodDto>();

                        CreateMap<CreateContactMethodDto, ContactMethod>();
                    }
                }
            }