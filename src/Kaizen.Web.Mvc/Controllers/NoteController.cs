using Kaizen.Controllers;
using Kaizen.EntityFrameworkCore;
using Kaizen.Web.Models.History;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kaizen.Web.Controllers
{
    //[AbpMvcAuthorize(PermissionNames.Pages_Addresses)]
    public class NoteController : KaizenControllerBase
    {
        private readonly KaizenDbContext _context;

        public NoteController(KaizenDbContext context)
        {
            _context = context;
        }

        public async Task<List<Kaizen.Entities.Notes.Dto.NoteDto>> GetNotesForEntity(string entityType, long entityId)
        {
            var notes = _context.Note.Where(a => a.EntityType == entityType && a.EntityId == entityId).ToList();

            // Hide internal notes to anyone not an admin.

            return ObjectMapper.Map<List<Kaizen.Entities.Notes.Dto.NoteDto>>(notes);
        }

        [HttpPost]
        public async Task<ActionResult> AddNoteToEntity(string entityType, long entityId, bool isInternal, string content)
        {
            var notes = _context.Note.Where(a => a.EntityType == entityType && a.EntityId == entityId).ToList();


            return Json(new { });
        }
    }
}
