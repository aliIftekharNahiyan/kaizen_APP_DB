using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Kaizen.Authorization;
using Kaizen.Controllers;
using Kaizen.EntityFrameworkCore;
using Kaizen.Users;
using Kaizen.Web.Models.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NReco.PdfGenerator;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Kaizen.Web.Controllers
{
    //[AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class DocumentsController : KaizenControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly KaizenDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DocumentsController(IUserAppService userAppService, KaizenDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _userAppService = userAppService;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<ActionResult> Index()
        {
            var model = new UserListViewModel
            {

            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(UserListViewModel model)
        {
            try
            {
                HtmlToPdfConverter htmlToPdf = new HtmlToPdfConverter();
                htmlToPdf.License.SetLicenseKey("PDF_Generator_Bin_Examples_Pack_206130553211", "eicr1Du58diPujXXoWVe0Zt6S5cy0eRKxfWCJd5+3Yam+jYZ68g8W7/i90BqWqqeqnxRLY+2LCcgD+uP2uIbpLKl8ckFoNMOJRNgLhZTpjm4WPDh7FWgQNCTSCEj/Oy0laSK1yoCXGDfyou/FeckuzZdZ5dYP8nRodUa2pWhZAA=");
                htmlToPdf.WkHtmlToPdfExeName = "wkhtmltopdf.exe";
                htmlToPdf.PdfToolPath = Path.Combine(_hostingEnvironment.WebRootPath, "wkhtmltopdf");
                htmlToPdf.Orientation = PageOrientation.Portrait;
                htmlToPdf.Size = PageSize.A4;
                htmlToPdf.Margins = new PageMargins { Top = 10, Bottom = 10, Left = 10, Right = 10 };

                var htmlContent = $"<img src='{model.Signature}' />";

                byte[] bytes = htmlToPdf.GeneratePdf(htmlContent);

                return File(bytes, "application/pdf", "Testing PDF.pdf");
            }
            catch(Exception ex)
            {

            }

            return View(model);
        }

        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = (await _userAppService.GetRoles()).Items;
            var user = await _userAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new UserEditViewModel
            {
                Roles = roles,
                User = user
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userAppService.UpdateAsync(model.User);
                }
                catch (Exception ex)
                {

                }
            }

            var roles = (await _userAppService.GetRoles()).Items;
            var user = await _userAppService.GetAsync(new EntityDto<long>(model.User.Id));

            model.Roles = roles;
            model.User = user;
 
            return View(model);
        }


        public async Task<ActionResult> EditModal(long userId)
        {
            var user = await _userAppService.GetAsync(new EntityDto<long>(userId));
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new EditUserModalViewModel
            {
                User = user,
                Roles = roles
            };
            return PartialView("_EditModal", model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}
