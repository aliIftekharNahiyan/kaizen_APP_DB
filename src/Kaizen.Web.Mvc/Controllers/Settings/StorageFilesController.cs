
using Abp.Application.Services.Dto;
using Kaizen.Controllers;
using Kaizen.Entities.StorageFiles;
using Kaizen.Web.Models.StorageFiles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kaizen.Web.Areas.Controllers
{
    public class StorageFilesController : KaizenControllerBase
    {
        private readonly IStorageFileAppService _storageFileAppService;

        public StorageFilesController(IStorageFileAppService storageFileAppService)
        {
            _storageFileAppService = storageFileAppService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new StorageFileListViewModel
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

            var storageFile = await _storageFileAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new StorageFileEditViewModel
            {
                StorageFile = storageFile
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(StorageFileEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _storageFileAppService.UpdateAsync(model.StorageFile);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long storageFileId)
        {
            Entities.StorageFiles.Dto.StorageFileDto storageFile = await _storageFileAppService.GetAsync(new EntityDto<long>(storageFileId));
            var model = new EditStorageFileModalViewModel
            {
                StorageFile = storageFile
            };
            return PartialView("_EditModal", model);
        }
    }
}