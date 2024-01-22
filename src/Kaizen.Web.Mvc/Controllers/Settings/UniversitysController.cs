
using Abp.Application.Services.Dto;
using Kaizen.Controllers;
using Kaizen.Entities.Universitys;
using Kaizen.Web.Models.Universitys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kaizen.Web.Areas.Controllers
{
    public class UniversitysController : KaizenControllerBase
    {
        private readonly IUniversityAppService _universityAppService;

        public UniversitysController(IUniversityAppService universityAppService)
        {
            _universityAppService = universityAppService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new UniversityListViewModel
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

            var university = await _universityAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new UniversityEditViewModel
            {
                University = university
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UniversityEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _universityAppService.UpdateAsync(model.University);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long universityId)
        {
            Entities.Universitys.Dto.UniversityDto university = await _universityAppService.GetAsync(new EntityDto<long>(universityId));
            var model = new EditUniversityModalViewModel
            {
                University = university
            };
            return PartialView("_EditModal", model);
        }
    }
}