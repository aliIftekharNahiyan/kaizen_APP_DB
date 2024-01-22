
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kaizen.Controllers;
using Kaizen.Entities.InfoCheckLists;
using Abp.Application.Services.Dto;
using Kaizen.Web.Models.InfoCheckList;
using Kaizen.Entities.InfoCheckLists.Dto;
using Azure;
using Kaizen.Entities.Companies.Dto;
using Kaizen.Entities.StorageFiles.Dto;
using Kaizen.Web.Models.Companies;
using Kaizen.Web.Services;
using System.IO;
using Kaizen.Web.Models.AcademicTerm;
using Abp.Runtime.Validation;

namespace Kaizen.Web.Areas.Controllers
{
    public class InfoCheckListsController : KaizenControllerBase
    {
        private readonly IInfoCheckListAppService _infoCheckListAppService;

        public InfoCheckListsController(IInfoCheckListAppService infoCheckListAppService)
        {
            _infoCheckListAppService = infoCheckListAppService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new InfoCheckListListViewModel
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

            var infoCheckList = await _infoCheckListAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new InfoCheckListEditViewModel
            {
                InfoCheckList = infoCheckList
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(InfoCheckListEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.InfoCheckList.Gender = "male";
                    model.InfoCheckList.DateAnswered = DateTime.Now;
                    await _infoCheckListAppService.UpdateAsync(model.InfoCheckList);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        [HttpGet]
        public async Task<ActionResult> Create()
        {
            InfoCheckListCreateViewModel model = new InfoCheckListCreateViewModel();

            return View(model);
        }


        [HttpPost]
        [DisableValidation]
        public async Task<ActionResult> Create(InfoCheckListCreateViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                      //  model.InfoCheckList.Gender = "male";
                        await _infoCheckListAppService.CreateAsync(model.InfoCheckList);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {

                    }
                }

                return View(model);

            }
            catch (Exception ex)
            {
                return BadRequest("File is required");

            }

        }
    }
}