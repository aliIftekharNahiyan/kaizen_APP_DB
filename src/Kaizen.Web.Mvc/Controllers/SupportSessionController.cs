
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kaizen.Controllers;
using Kaizen.Entities.SupportSessions;
using Abp.Application.Services.Dto;
using Kaizen.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Extensions;
using System.Linq;
using Kaizen.Entities.SupportSessions.Dto;
using Kaizen.Web.Models.SupportSessions;

namespace Kaizen.Web.Areas.Controllers
{
    public class SupportSessionsController : KaizenControllerBase
    {
        private readonly ISupportSessionAppService _supportSessionAppService;
        private readonly KaizenDbContext _context;

        public SupportSessionsController(ISupportSessionAppService sessionGroupAppService, KaizenDbContext context)
        {
            _supportSessionAppService = sessionGroupAppService;
            _context = context;
        }

        [Route("Sessions/SupportSessions")]
        public async Task<ActionResult> Index()
        {
            var model = new SupportSessionListViewModel
            {

            };
            return View(model);
        }

        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionGroup = await _supportSessionAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new SupportSessionEditViewModel
            {
                SupportSession = sessionGroup
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SupportSessionEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _supportSessionAppService.UpdateAsync(model.SupportSession);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long supportSessionId)
        {
            SupportSessionDto sessionGroup = ObjectMapper.Map<SupportSessionDto>(_context.SupportSession.AsNoTracking().Include(a => a.SupportType).Include(a => a.FundingBody).Include(a => a.SessionGroup).Include(a => a.SessionType).Include(a => a.SupportProfessionalUser).SingleOrDefault(a => a.Id == supportSessionId));
            var model = new EditSupportSessionModalViewModel
            {
                SupportSession = sessionGroup
            };
            return PartialView("_EditModal", model);
        }
    }
}