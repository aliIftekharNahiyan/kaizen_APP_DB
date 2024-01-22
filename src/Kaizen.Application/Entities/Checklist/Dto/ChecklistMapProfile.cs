
            using AutoMapper;
            using Kaizen.Entities.Checklists;
            using Kaizen.Entities.Checklists.Dto;

            namespace Kaizen.Users.Dto
            {
                public class ChecklistMapProfile : Profile
                {
                    public ChecklistMapProfile()
                    {
                        CreateMap<Checklist, ChecklistDto>();
                        CreateMap<Checklist, CreateChecklistDto>();

                        CreateMap<CreateChecklistDto, Checklist>();
                    }
                }
            }