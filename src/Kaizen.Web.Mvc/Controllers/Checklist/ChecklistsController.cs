
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kaizen.Controllers;
using Kaizen.Entities.Checklists;
using Kaizen.Web.Models.Checklists;
using Abp.Application.Services.Dto;

namespace Kaizen.Web.Areas.Controllers
{
    public class ChecklistsController : KaizenControllerBase
    {
        private readonly IChecklistAppService _checklistAppService;

        public ChecklistsController(IChecklistAppService checklistAppService)
        {
            _checklistAppService = checklistAppService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new ChecklistListViewModel
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

            var checklist = await _checklistAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new ChecklistEditViewModel
            {
                Checklist = checklist
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ChecklistEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _checklistAppService.UpdateAsync(model.Checklist);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long checklistId)
        {
            Entities.Checklists.Dto.ChecklistDto checklist = await _checklistAppService.GetAsync(new EntityDto<long>(checklistId));
            var model = new EditChecklistModalViewModel
            {
                Checklist = checklist
            };
            return PartialView("_EditModal", model);
        }
    }
}