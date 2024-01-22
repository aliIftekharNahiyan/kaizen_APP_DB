
using Abp.Application.Services.Dto;
using Kaizen.Controllers;
using Kaizen.Entities.Regions;
using Kaizen.Web.Models.Regions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kaizen.Web.Areas.Controllers
{
    public class RegionsController : KaizenControllerBase
    {
        private readonly IRegionAppService _regionAppService;

        public RegionsController(IRegionAppService regionAppService)
        {
            _regionAppService = regionAppService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new RegionListViewModel
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

            var region = await _regionAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new RegionEditViewModel
            {
                Region = region
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RegionEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _regionAppService.UpdateAsync(model.Region);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long regionId)
        {
            Entities.Regions.Dto.RegionDto region = await _regionAppService.GetAsync(new EntityDto<long>(regionId));
            var model = new EditRegionModalViewModel
            {
                Region = region
            };
            return PartialView("_EditModal", model);
        }
    }
}