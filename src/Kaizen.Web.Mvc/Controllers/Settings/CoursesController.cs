
using Abp.Application.Services.Dto;
using Kaizen.Controllers;
using Kaizen.Entities.Courses;
using Kaizen.Web.Models.Course;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kaizen.Web.Areas.Controllers
{
    public class CoursesController : KaizenControllerBase
    {
        private readonly ICourseAppService _universityAppService;

        public CoursesController(ICourseAppService universityAppService)
        {
            _universityAppService = universityAppService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new CourseListViewModel
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
            var model = new CourseEditViewModel
            {
                Course = university
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CourseEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _universityAppService.UpdateAsync(model.Course);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long universityId)
        {
            Entities.Courses.Dto.CourseDto university = await _universityAppService.GetAsync(new EntityDto<long>(universityId));
            var model = new EditCourseModalViewModel
            {
                Course = university
            };
            return PartialView("_EditModal", model);
        }
    }
}