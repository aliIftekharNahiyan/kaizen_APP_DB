
using Abp.Application.Services.Dto;
using Abp.MimeTypes;
using Azure;
using Kaizen.Controllers;
using Kaizen.Entities.Companies;
using Kaizen.Entities.Companies.Dto;
using Kaizen.Entities.StorageFiles;
using Kaizen.Entities.StorageFiles.Dto;
using Kaizen.Web.Models.Companies;
using Kaizen.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Kaizen.Web.Areas.Controllers
{
    public class CompaniesController : KaizenControllerBase
    {
        private readonly ICompanyAppService _companyAppService;

        private readonly IStorageFileAppService _storageFileAppService;
        private readonly ICloudFileStorage _cloudFileStorage;
        private readonly IMimeTypeMap _mimeTypeMap;
        public CompaniesController(ICompanyAppService companyAppService, IStorageFileAppService storageFileAppService,ICloudFileStorage cloudFileStorage, IMimeTypeMap mimeTypeMap)
        {
            _companyAppService = companyAppService;
            _storageFileAppService = storageFileAppService;
            _cloudFileStorage = cloudFileStorage;
            _mimeTypeMap = mimeTypeMap;
        }

        public async Task<ActionResult> Index()
        {
            var model = new CompanyListViewModel{};
            return View(model);
        }

        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _companyAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new CompanyEditViewModel
            {
                Company = company
            };

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            CompanyCreateViewModel model = new CompanyCreateViewModel();
            
            return PartialView("_CreateModal", model);
        }

        private const string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=kaizenstorageaccount;AccountKey=hM9NNdLpD5PMRtjydhQNhIjuz03BOpBgkxtIpGDv+Y5jXHevdbukIjIxKpY3ETIEspn8Sl+GfYuy+AStGVh8GQ==;EndpointSuffix=core.windows.net";
        private const string ContainerName = "kaizenstorageaccount";

        [HttpPost]        
        public async Task<ActionResult> Create( CreateCompanyDto model)
        {
        
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }


                if (model.LogoFile != null && model.LogoFile.Length > 0)
                {
                    try
                    {
                        // Azure file upload
                        var blobName = Guid.NewGuid().ToString() + Path.GetExtension(model.LogoFile.FileName);
                        var blobClient = await _cloudFileStorage.UploadFileAsync(model.LogoFile, blobName);
                        
                        CreateStorageFileDto storageDto = new CreateStorageFileDto()
                        {
                            Name = "Logo",
                            FileUrl = blobClient.Uri.AbsoluteUri,
                            Description = model.Description,
                            FileName = blobClient.Name,
                            MimeType = Path.GetExtension(model.LogoFile.FileName),

                        };
                        var sfile = await _storageFileAppService.CreateAsync(storageDto);

                        model.LogoFileId = sfile.Id;

                        await _companyAppService.CreateAsync(model);

                    }
                    catch (RequestFailedException ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return BadRequest("File is required");
                }             

            }
            catch (Exception ex)
            {
                return BadRequest("File is required");

            }

            return Ok(model);
        }

     
        [HttpPost]
        public async Task<ActionResult> Edit(CompanyDto model)
           {

            try
             {
                 if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (model.LogoFile == null && model.LogoFileId < 0)
                {
                    return BadRequest("Select logo file");
                }


                if (model.LogoFile != null)
                {
                    try
                    {
                        
                        // Azure file upload
                        var blobName = Guid.NewGuid().ToString() + Path.GetExtension(model.LogoFile.FileName);
                        var blobClient = await _cloudFileStorage.UploadFileAsync(model.LogoFile, blobName);

                        // delete previous blob file                        

                        var storageDto = await _storageFileAppService.GetAsync(new EntityDto<long>(model.LogoFileId));

                        // if exist then update
                        if (storageDto != null)
                        {
                            storageDto.Name = "Logo";
                            storageDto.FileUrl = blobClient.Uri.AbsoluteUri;
                            storageDto.Description = model.Description;
                            storageDto.FileName = blobClient.Name;
                            storageDto.MimeType = Path.GetExtension(model.LogoFile.FileName);
                            storageDto.Id = model.LogoFileId;
                            // file update
                            storageDto = await _storageFileAppService.UpdateAsync(storageDto);
                        }
                        else
                        {
                            // if not exist then create
                            var cStorageDto = new CreateStorageFileDto()
                            {
                                Name = "Logo",
                                FileUrl = blobClient.Uri.AbsoluteUri,
                                Description = model.Description,
                                FileName = blobClient.Name,
                                MimeType = Path.GetExtension(model.LogoFile.FileName),
                                Id = model.LogoFileId,
                            };
                            storageDto = await _storageFileAppService.CreateAsync(cStorageDto);
                        }



                        model.LogoFileId = storageDto.Id;
                        // copmany update
                        await _companyAppService.UpdateAsync(model);

                    }
                    catch (RequestFailedException ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else if (model.LogoFileId > 0)
                {
                    // copmany update
                    await _companyAppService.UpdateAsync(model);
                }
                else
                {
                    return BadRequest("File is required");
                }

            }
            catch (Exception ex)
            {
                return BadRequest("File is required");

            }

            return Ok(model);
        }

        public async Task<ActionResult> EditModal(long companyId)
        {            
            var model = await _companyAppService.GetAsync(new EntityDto<long>(companyId));

            return PartialView("_EditModal", model);
        }

        public async Task<IActionResult> DisplayImage(long id)
        {            
            var storageFile = await _storageFileAppService.GetAsync(new EntityDto<long>(id));
            var fileStream = await _cloudFileStorage.DownloadFileAsync(storageFile.FileName);
            
            string contentType = _mimeTypeMap.GetMimeType(Path.GetExtension(storageFile.FileName));
            return File(fileStream, contentType);
        }

    }
}