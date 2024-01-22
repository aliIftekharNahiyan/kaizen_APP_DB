
using AutoMapper;
using Kaizen.Entities.ChecklistItems;
using Kaizen.Entities.ChecklistItems.Dto;
using Kaizen.Entities.Checklists;
using Kaizen.Entities.LinkTables;

namespace Kaizen.Users.Dto
{
    public class ChecklistItemMapProfile : Profile
    {
        public ChecklistItemMapProfile()
        {
            CreateMap<ChecklistItem, ChecklistItemDto>();
            CreateMap<ChecklistItem, CreateChecklistItemDto>();
            CreateMap<ChecklistItemOption, ChecklistItemOptionDto>();
            CreateMap<ChecklistCheckListItem, ChecklistCheckListItemDto>();

            CreateMap<CreateChecklistItemDto, ChecklistItem>();
            CreateMap<ChecklistItemOptionDto, ChecklistItemOption>();
            CreateMap<ChecklistCheckListItemDto, ChecklistCheckListItem>();
        }
    }
}