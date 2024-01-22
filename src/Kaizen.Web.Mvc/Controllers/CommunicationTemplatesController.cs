using Abp.Application.Services.Dto;
using Kaizen.Controllers;
using Kaizen.Entities.Addresses;
using Kaizen.Entities.CommunicationGroups;
using Kaizen.Entities.CommunicationGroups.Dto;
using Kaizen.Entities.CommunicationTemplates;
using Kaizen.Entities.CommunicationTemplates.Dto;
using Kaizen.EntityFrameworkCore;
using Kaizen.Enums;
using Kaizen.Web.Models.Addresses;
using Kaizen.Web.Models.CommunicationGroup;
using Kaizen.Web.Models.CommunicationTemplate;
using Kaizen.Web.Models.Users;
using Kaizen.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kaizen.Web.Areas.Controllers
{
    //[AbpMvcAuthorize(PermissionNames.Pages_Addresses)]
    public class CommunicationTemplatesController : KaizenControllerBase
    {
        private readonly ICommunicationTemplateAppService _communicationTemplateAppService;
        private readonly ICloudFileStorage _cloudStorageService;
        private readonly KaizenDbContext _context;

        public CommunicationTemplatesController(ICommunicationTemplateAppService communicationTemplateAppService,
            ICloudFileStorage cloudStorageService,
            KaizenDbContext context)
        {
            _communicationTemplateAppService = communicationTemplateAppService;
            _cloudStorageService = cloudStorageService;
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var model = new CommunicationTemplateListViewModel
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

            Entities.CommunicationTemplates.Dto.CommunicationTemplateDto communicationTemplate = await _communicationTemplateAppService.GetAsync(new EntityDto<long>(id.Value));

            var fileContents = await _cloudStorageService.ReadTextFromFile(communicationTemplate.TemplateHTMLContentUrl);

            communicationTemplate.HTMLContent = fileContents;

            var model = new CommunicationTemplateEditViewModel
            {
               CommunicationTemplate = communicationTemplate,
            };

            PopulateViewModel(model);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CommunicationTemplateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Only replace if the content has actually changed.
                    Entities.CommunicationTemplates.Dto.CommunicationTemplateDto communicationTemplate = await _communicationTemplateAppService.GetAsync(new EntityDto<long>(model.CommunicationTemplate.Id));

                    var fileContents = await _cloudStorageService.ReadTextFromFile(communicationTemplate.TemplateHTMLContentUrl);

                    var fileName = "templates/" + model.CommunicationTemplate.Name + "_" + DateTime.Now.Ticks + ".html";

                    if (model.HTMLFileUpload != null)
                    {
                        // Upload HTML content into a file
                        var blobResponse = await _cloudStorageService.UploadFileAsync(model.HTMLFileUpload, fileName);

                        model.CommunicationTemplate.TemplateHTMLContentUrl = fileName;

                        await _cloudStorageService.DeleteFileAsync(communicationTemplate.TemplateHTMLContentUrl);
                    }
                    else if (fileContents != model.CommunicationTemplate.HTMLContent)
                    {
                        // Upload HTML content into a file
                        var blobResponse = await _cloudStorageService.UploadFileAsync(model.CommunicationTemplate.HTMLContent, fileName);

                        model.CommunicationTemplate.TemplateHTMLContentUrl = fileName;

                        await _cloudStorageService.DeleteFileAsync(communicationTemplate.TemplateHTMLContentUrl);
                    }

                    await _communicationTemplateAppService.UpdateAsync(model.CommunicationTemplate);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                }
            }

            PopulateViewModel(model);

            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            var model = new CreateCommunicationTemplateModalViewModel
            {

            };

            PopulateViewModel(model);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCommunicationTemplateModalViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fileName = "templates/" + model.CommunicationTemplate.Name + "_" + DateTime.Now.Ticks + ".html";

                    if (model.HTMLFileUpload != null)
                    {
                        // Upload HTML content into a file
                        var blobResponse = await _cloudStorageService.UploadFileAsync(model.HTMLFileUpload, fileName);

                        model.CommunicationTemplate.TemplateHTMLContentUrl = fileName;
                    }
                    else
                    {
                        // Upload HTML content into a file
                        var blobResponse = await _cloudStorageService.UploadFileAsync(model.CommunicationTemplate.HTMLContent, fileName);

                        model.CommunicationTemplate.TemplateHTMLContentUrl = fileName;
                    }

                    await _communicationTemplateAppService.CreateAsync(model.CommunicationTemplate);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                }
            }

            PopulateViewModel(model);

            return View(model);
        }

        public async Task<ActionResult> DownloadHTML(int id)
        {
            Entities.CommunicationTemplates.Dto.CommunicationTemplateDto communicationTemplate = await _communicationTemplateAppService.GetAsync(new EntityDto<long>(id));

            var fileContents = await _cloudStorageService.DownloadFileAsync(communicationTemplate.TemplateHTMLContentUrl);

            return File(fileContents, "text/html");
        }

        public async Task<CommunicationTemplateDto> GetTemplate(int id)
        {
            Entities.CommunicationTemplates.Dto.CommunicationTemplateDto communicationTemplate = await _communicationTemplateAppService.GetAsync(new EntityDto<long>(id));

            return communicationTemplate;
        }

        private void PopulateViewModel(CommunicationTemplateBaseViewModel model)
        {
            model.HeaderTemplates = _context.CommunicationTemplate.Where(a => a.TemplateArea == CommunicationTemplateArea.Header).Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToList();

            model.FooterTemplates = _context.CommunicationTemplate.Where(a => a.TemplateArea == CommunicationTemplateArea.Footer).Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToList();
        }
    }
}
