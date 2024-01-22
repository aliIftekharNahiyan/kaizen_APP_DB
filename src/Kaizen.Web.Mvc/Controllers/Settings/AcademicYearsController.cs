
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kaizen.Controllers;
using Kaizen.Entities.AcademicYears;
using Abp.Application.Services.Dto;
using Kaizen.Web.Models.AcademicYear;

namespace Kaizen.Web.Areas.Controllers
{
    public class AcademicYearsController : KaizenControllerBase
    {
        private readonly IAcademicYearAppService _academicYearAppService;

        public AcademicYearsController(IAcademicYearAppService academicYearAppService)
        {
            _academicYearAppService = academicYearAppService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new AcademicYearListViewModel
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

            var academicYear = await _academicYearAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new AcademicYearEditViewModel
            {
                AcademicYear = academicYear
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(AcademicYearEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _academicYearAppService.UpdateAsync(model.AcademicYear);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long academicYearId)
        {
            Entities.AcademicYears.Dto.AcademicYearDto academicYear = await _academicYearAppService.GetAsync(new EntityDto<long>(academicYearId));
            var model = new EditAcademicyearModalViewModel
            {
                AcademicYear = academicYear
            };
            return PartialView("_EditModal", model);
        }
    }
}