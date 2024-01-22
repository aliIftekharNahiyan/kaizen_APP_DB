
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kaizen.Controllers;
using Kaizen.Entities.CourseTerms;
using Abp.Application.Services.Dto;
using Kaizen.Web.Models.CourseTerm;
using Kaizen.Entities.AcademicYears;

namespace Kaizen.Web.Areas.Controllers
{
    public class CourseTermsController : KaizenControllerBase
    {
        private readonly ICourseTermAppService _courseTermAppService;

        public CourseTermsController(ICourseTermAppService courseTermAppService)
        {
            _courseTermAppService = courseTermAppService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new CourseTermListViewModel
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

            var courseTerm = await _courseTermAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new CourseTermEditViewModel
            {
                CourseTerm = courseTerm
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CourseTermEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _courseTermAppService.UpdateAsync(model.CourseTerm);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long courseTermId)
        {
            Entities.CourseTerms.Dto.CourseTermDto courseTerm = await _courseTermAppService.GetAsync(new EntityDto<long>(courseTermId));
            var model = new EditCourseTermModalViewModel
            {
                CourseTerm = courseTerm
            };
            return PartialView("_EditModal", model);
        }
    }
}