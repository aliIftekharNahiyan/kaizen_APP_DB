
using Abp.Application.Services.Dto;
using Kaizen.Controllers;
using Kaizen.Entities.ContactMethods;
using Kaizen.Web.Models.ContactMethods;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kaizen.Web.Areas.Controllers
{
    public class ContactMethodsController : KaizenControllerBase
    {
        private readonly IContactMethodAppService _contactMethodAppService;

        public ContactMethodsController(IContactMethodAppService contactMethodAppService)
        {
            _contactMethodAppService = contactMethodAppService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new ContactMethodListViewModel
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

            var contactMethod = await _contactMethodAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new ContactMethodEditViewModel
            {
                ContactMethod = contactMethod
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ContactMethodEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _contactMethodAppService.UpdateAsync(model.ContactMethod);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long contactMethodId)
        {
            Entities.ContactMethods.Dto.ContactMethodDto contactMethod = await _contactMethodAppService.GetAsync(new EntityDto<long>(contactMethodId));
            var model = new EditContactMethodModalViewModel
            {
                ContactMethod = contactMethod
            };
            return PartialView("_EditModal", model);
        }
    }
}