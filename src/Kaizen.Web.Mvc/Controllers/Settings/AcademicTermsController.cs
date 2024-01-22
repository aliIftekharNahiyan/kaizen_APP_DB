
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kaizen.Controllers;
using Kaizen.Entities.AcademicTerms;
using Abp.Application.Services.Dto;
using Kaizen.Web.Models.AcademicTerm;
using Kaizen.Entities.AcademicTerms.Dto;

namespace Kaizen.Web.Areas.Controllers
{
    public class AcademicTermsController : KaizenControllerBase
    {
        private readonly IAcademicTermAppService _academicTermAppService;

        public AcademicTermsController(IAcademicTermAppService academicTermAppService)
        {
            _academicTermAppService = academicTermAppService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new AcademicTermListViewModel
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

            var academicTerm = await _academicTermAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new AcademicTermEditViewModel
            {
                AcademicTerm = academicTerm
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(AcademicTermEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _academicTermAppService.UpdateAsync(model.AcademicTerm);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long academicTermId)
        {
            AcademicTermDto academicTerm = await _academicTermAppService.GetAsync(new EntityDto<long>(academicTermId));

            var model = new EditAcademicTermModalViewModel
            {
                AcademicTerm = academicTerm
            };
            return PartialView("_EditModal", model);
        }
    }
}