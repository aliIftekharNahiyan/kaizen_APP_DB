
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kaizen.Controllers;
using Kaizen.Entities.FundingBodys;
using Kaizen.Web.Models.FundingBodys;
using Abp.Application.Services.Dto;

namespace Kaizen.Web.Areas.Controllers
{
    public class FundingBodysController : KaizenControllerBase
    {
        private readonly IFundingBodyAppService _fundingBodyAppService;

        public FundingBodysController(IFundingBodyAppService fundingBodyAppService)
        {
            _fundingBodyAppService = fundingBodyAppService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new FundingBodyListViewModel
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

            var fundingBody = await _fundingBodyAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new FundingBodyEditViewModel
            {
                FundingBody = fundingBody
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(FundingBodyEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _fundingBodyAppService.UpdateAsync(model.FundingBody);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long fundingBodyId)
        {
            Entities.FundingBodys.Dto.FundingBodyDto fundingBody = await _fundingBodyAppService.GetAsync(new EntityDto<long>(fundingBodyId));
            var model = new EditFundingBodyModalViewModel
            {
                FundingBody = fundingBody
            };
            return PartialView("_EditModal", model);
        }
    }
}