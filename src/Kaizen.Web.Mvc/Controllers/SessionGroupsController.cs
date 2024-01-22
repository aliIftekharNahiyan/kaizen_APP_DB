
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kaizen.Controllers;
using Kaizen.Entities.SessionGroups;
using Kaizen.Web.Models.SessionGroups;
using Abp.Application.Services.Dto;
using Kaizen.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Extensions;
using System.Linq;
using Kaizen.Entities.SessionGroups.Dto;

namespace Kaizen.Web.Areas.Controllers
{
    public class SessionGroupsController : KaizenControllerBase
    {
        private readonly ISessionGroupAppService _sessionGroupAppService;
        private readonly KaizenDbContext _context;

        public SessionGroupsController(ISessionGroupAppService sessionGroupAppService, KaizenDbContext context)
        {
            _sessionGroupAppService = sessionGroupAppService;
            _context = context;
        }

        [Route("Sessions/SessionGroups")]
        public async Task<ActionResult> Index()
        {
            var model = new SessionGroupListViewModel
            {

            };
            return View(model);
        }

        public async Task<ActionResult> IndexBP()
        {
            var model = new SessionGroupListViewModel
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

            var sessionGroup = await _sessionGroupAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new SessionGroupEditViewModel
            {
                SessionGroup = sessionGroup
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SessionGroupEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _sessionGroupAppService.UpdateAsync(model.SessionGroup);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long sessionGroupId)
        {
            SessionGroupDto sessionGroup = ObjectMapper.Map<SessionGroupDto>(_context.SessionGroup.AsNoTracking().Include(a => a.SupportType).Include(a => a.FundingBody).SingleOrDefault(a => a.Id == sessionGroupId));
            var model = new EditSessionGroupModalViewModel
            {
                SessionGroup = sessionGroup
            };
            return PartialView("_EditModal", model);
        }
    }
}