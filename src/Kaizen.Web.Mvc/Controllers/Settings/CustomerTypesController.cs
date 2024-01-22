
using Abp.Application.Services.Dto;
using Kaizen.Controllers;
using Kaizen.Entities.CustomerTypes;
using Kaizen.Web.Models.CustomerTypes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kaizen.Web.Areas.Controllers
{
    public class CustomerTypesController : KaizenControllerBase
    {
        private readonly ICustomerTypeAppService _customerTypeAppService;

        public CustomerTypesController(ICustomerTypeAppService customerTypeAppService)
        {
            _customerTypeAppService = customerTypeAppService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new CustomerTypeListViewModel
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

            var customerType = await _customerTypeAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new CustomerTypeEditViewModel
            {
                CustomerType = customerType
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CustomerTypeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _customerTypeAppService.UpdateAsync(model.CustomerType);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long customerTypeId)
        {
            Entities.CustomerTypes.Dto.CustomerTypeDto customerType = await _customerTypeAppService.GetAsync(new EntityDto<long>(customerTypeId));
            var model = new EditCustomerTypeModalViewModel
            {
                CustomerType = customerType
            };
            return PartialView("_EditModal", model);
        }
    }
}