using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Kaizen.Authorization;
using Kaizen.Controllers;
using Kaizen.Entities.SupportTypes;
using Kaizen.EntityFrameworkCore;
using Kaizen.Web.Models.SupportTypes;
using Kaizen.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kaizen.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class SupportTypesController : KaizenControllerBase
    {
        private readonly ISupportTypeAppService _supportTypeAppService;
        private readonly KaizenDbContext _context;

        public SupportTypesController(ISupportTypeAppService supportTypeAppService, KaizenDbContext context)
        {
            _supportTypeAppService = supportTypeAppService;
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var model = new SupportTypeListViewModel
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

            var supportType = await _supportTypeAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new SupportTypeEditViewModel
            {
                SupportType = supportType
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SupportTypeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _supportTypeAppService.UpdateAsync(model.SupportType);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long supportTypeId)
        {
            Entities.SupportTypes.Dto.SupportTypeDto supportType = await _supportTypeAppService.GetAsync(new EntityDto<long>(supportTypeId));
            var model = new EditSupporTypeModalViewModel
            {
                SupportType = supportType
            };
            return PartialView("_EditModal", model);
        }
    }
}
