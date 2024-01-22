using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Kaizen.Entities.Notes.Dto;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Kaizen.Entities.Notes
{
    public interface INoteAppService : IAsyncCrudAppService<NoteDto, long, PagedNoteResultRequestDto, CreateNoteDto, NoteDto>
    {
        Task<PagedResultDto<IGrouping<string, NoteDto>>> GetGroupedAsync(PagedNoteResultRequestDto input);
    }
 }