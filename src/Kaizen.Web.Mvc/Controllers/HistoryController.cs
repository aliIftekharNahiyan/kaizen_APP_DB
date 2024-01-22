using Kaizen.Controllers;
using Kaizen.EntityFrameworkCore;
using Kaizen.Web.Models.History;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kaizen.Web.Controllers
{
    //[AbpMvcAuthorize(PermissionNames.Pages_Addresses)]
    public class HistoryController : KaizenControllerBase
    {
        private readonly KaizenDbContext _context;

        public HistoryController(KaizenDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> EditModal(long changesetId)
        {
            var model = new EditHistoryModalViewModel
            {
                History = new Entities.History.Dto.HistoryChangesetDto
                {
                    Id = changesetId
                }
            };

            return PartialView("_EditModal", model);
        }
    }
}
