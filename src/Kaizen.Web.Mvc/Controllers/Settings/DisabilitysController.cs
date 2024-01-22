
using Abp.Application.Services.Dto;
using Kaizen.Controllers;
using Kaizen.Entities.Disabilitys;
using Kaizen.Web.Models.Disabilitys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kaizen.Web.Areas.Controllers
{
    public class DisabilitysController : KaizenControllerBase
    {
        private readonly IDisabilityAppService _disabilityAppService;

        public DisabilitysController(IDisabilityAppService disabilityAppService)
        {
            _disabilityAppService = disabilityAppService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new DisabilityListViewModel
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

            var disability = await _disabilityAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new DisabilityEditViewModel
            {
                Disability = disability
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(DisabilityEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _disabilityAppService.UpdateAsync(model.Disability);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long disabilityId)
        {
            Entities.Disabilitys.Dto.DisabilityDto disability = await _disabilityAppService.GetAsync(new EntityDto<long>(disabilityId));
            var model = new EditDisabilityModalViewModel
            {
                Disability = disability
            };
            return PartialView("_EditModal", model);
        }
    }
}