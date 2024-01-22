
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kaizen.Controllers;
using Kaizen.Entities.ChecklistItems;
using Kaizen.Web.Models.ChecklistItems;
using Abp.Application.Services.Dto;
using Kaizen.EntityFrameworkCore;
using Kaizen.Entities.ChecklistItems.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kaizen.Web.Areas.Controllers
{
    public class ChecklistItemsController : KaizenControllerBase
    {
        private readonly IChecklistItemAppService _checklistItemAppService;
        private readonly KaizenDbContext _context;

        public ChecklistItemsController(IChecklistItemAppService checklistItemAppService, KaizenDbContext context)
        {
            _checklistItemAppService = checklistItemAppService;
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var model = new ChecklistItemListViewModel
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

            var checklistItem = await _checklistItemAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new ChecklistItemEditViewModel
            {
                ChecklistItem = checklistItem
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ChecklistItemEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _checklistItemAppService.UpdateAsync(model.ChecklistItem);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long checklistItemId)
        {
            try
            {
                var checkListItem = _context.ChecklistItem.AsNoTracking().Include(a => a.Options).SingleOrDefault(a => a.Id == checklistItemId);

                ChecklistItemDto checklistItem = ObjectMapper.Map<ChecklistItemDto>(checkListItem);

                var model = new EditChecklistItemModalViewModel
                {
                    ChecklistItem = checklistItem
                };

                return PartialView("_EditModal", model);
            }
            catch(Exception ex)
            {

            }

            return PartialView("_EditModal", new EditChecklistItemModalViewModel { });
        }
    }
}