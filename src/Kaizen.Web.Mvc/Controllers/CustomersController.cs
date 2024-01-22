using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Kaizen.Authorization;
using Kaizen.Controllers;
using Kaizen.EntityFrameworkCore;
using Kaizen.Users;
using Kaizen.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kaizen.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class CustomersController : KaizenControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly KaizenDbContext _context;

        public CustomersController(IUserAppService userAppService, KaizenDbContext context)
        {
            _userAppService = userAppService;
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new UserListViewModel
            {
                Roles = roles
            };
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

            model.Roles = roles;

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
