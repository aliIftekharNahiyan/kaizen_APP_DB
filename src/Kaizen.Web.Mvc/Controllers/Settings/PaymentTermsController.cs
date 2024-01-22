
using Abp.Application.Services.Dto;
using Kaizen.Controllers;
using Kaizen.Entities.PaymentTerms;
using Kaizen.Web.Models.PaymentTerms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kaizen.Web.Areas.Controllers
{
    public class PaymentTermsController : KaizenControllerBase
    {
        private readonly IPaymentTermAppService _paymentTermAppService;

        public PaymentTermsController(IPaymentTermAppService paymentTermAppService)
        {
            _paymentTermAppService = paymentTermAppService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new PaymentTermListViewModel
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

            var paymentTerm = await _paymentTermAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new PaymentTermEditViewModel
            {
                PaymentTerm = paymentTerm
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(PaymentTermEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _paymentTermAppService.UpdateAsync(model.PaymentTerm);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long paymentTermId)
        {
            Entities.PaymentTerms.Dto.PaymentTermDto paymentTerm = await _paymentTermAppService.GetAsync(new EntityDto<long>(paymentTermId));
            var model = new EditPaymentTermModalViewModel
            {
                PaymentTerm = paymentTerm
            };
            return PartialView("_EditModal", model);
        }
    }
}