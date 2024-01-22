
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kaizen.Controllers;
using Kaizen.Entities.FundingBodys;
using Kaizen.Web.Models.FundingBodys;
using Abp.Application.Services.Dto;
using Kaizen.Entities.SessionTypes;
using Kaizen.Web.Models.SessionTypes;

namespace Kaizen.Web.Areas.Controllers
{
    public class SessionTypesController : KaizenControllerBase
    {
        private readonly ISessionTypeAppService _sessionTypeAppService;

        public SessionTypesController(ISessionTypeAppService sessionTypeAppService)
        {
            _sessionTypeAppService = sessionTypeAppService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new SessionTypeListViewModel
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

            var sessionType = await _sessionTypeAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new SessionTypeEditViewModel
            {
                SessionType = sessionType
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SessionTypeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _sessionTypeAppService.UpdateAsync(model.SessionType);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long sessionTypeId)
        {
            Entities.SessionTypes.Dto.SessionTypeDto sessionType = await _sessionTypeAppService.GetAsync(new EntityDto<long>(sessionTypeId));
            var model = new EditSessionTypeModalViewModel
            {
                SessionType = sessionType
            };
            return PartialView("_EditModal", model);
        }
    }
}