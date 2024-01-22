using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Kaizen.Authorization;
using Kaizen.Controllers;
using Kaizen.Entities.LinkTables;
using Kaizen.Entities.Lookups;
using Kaizen.EntityFrameworkCore;
using Kaizen.Users;
using Kaizen.Users.Dto;
using Kaizen.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kaizen.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class UsersController : KaizenControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly ILookupAppService _userTypeAppService;
        private readonly KaizenDbContext _context;

        public UsersController(IUserAppService userAppService, KaizenDbContext context, ILookupAppService userTypeAppService)
        {
            _userAppService = userAppService;
            _context = context;
            _userTypeAppService = userTypeAppService;
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

            //user.UserType = _userTypeAppService.GetAsync(new EntityDto<long>(user.UserTypeId));
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
            int flag = 1;
            if (ModelState.IsValid)
            {

                try
                {
                    List<UserSkillDto> usList = new List<UserSkillDto>();
                    foreach (long id in model.User.SkillId)
                    {
                        UserSkillDto us = new UserSkillDto();
                        us.UserId = model.User.Id;
                        us.SkillId = id;
                        us.CreatedBy = model.User.Id;
                        us.CreatedDate = DateTime.Now;
                        us.IsActive = true;
                        us.IsDeleted = false;
                        usList.Add(us);
                    }
                    List<UserLivingRegionLocationDto> ulrList = new List<UserLivingRegionLocationDto>();
                    foreach (long id in model.User.LRegionId)
                    {
                        UserLivingRegionLocationDto region = new UserLivingRegionLocationDto();
                        region.UserId = model.User.Id;
                        region.RegionLocationId = id;

                        ulrList.Add(region);
                    }
                    List<UserWorkRegionLocationDto> uwrList = new List<UserWorkRegionLocationDto>();
                    foreach (long id in model.User.WRegionId)
                    {
                        UserWorkRegionLocationDto region = new UserWorkRegionLocationDto();
                        region.UserId = model.User.Id;
                        region.RegionLocationId = id;

                        uwrList.Add(region);
                    }
                    List<UserClientSupportDto> ucsList = new List<UserClientSupportDto>();
                    foreach (long id in model.User.UserClientSupportId)
                    {
                        UserClientSupportDto ucs = new UserClientSupportDto();
                        ucs.UserId = model.User.Id;
                        ucs.ClientId = id;

                        ucsList.Add(ucs);
                    }
                    model.User.Skills = usList;
                    model.User.UserClientSupports = ucsList;
                    model.User.LivingRegionLocations = ulrList;
                    model.User.WorkRegionLocations = uwrList;

                    await _userAppService.UpdateAsync(model.User);
                }
                catch (Exception ex)
                {
                    flag = 0;
                }
            }

            var roles = (await _userAppService.GetRoles()).Items;
            var user = await _userAppService.GetAsync(new EntityDto<long>(model.User.Id));

            model.IsSuccess = flag;
            model.Roles = roles;
            model.User = user;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditRoles(UserEditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userAppService.UpdateRolesAsync(model.UserId, model.Roles.Select(a => a.Name).ToArray());
                }
                catch (Exception ex)
                {

                }
            }

            return RedirectToAction("Edit", model.UserId);
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
