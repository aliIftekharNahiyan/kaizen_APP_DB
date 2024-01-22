using AutoMapper;

namespace Kaizen.Entities.Notes.Dto
{
    public class NoteMapProfile : Profile
    {
        public NoteMapProfile()
        {
            CreateMap<NoteDto, Note>();
            CreateMap<Note, NoteDto>();
            CreateMap<Note, NoteDto>() .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Content));
            CreateMap<CreateNoteDto, Note>();
        }
    }
}