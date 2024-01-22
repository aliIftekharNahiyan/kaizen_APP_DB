using Abp.Application.Services.Dto;
using Kaizen.Controllers;
using Kaizen.Entities.UserKinAppService;
using Kaizen.EntityFrameworkCore;
using Kaizen.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kaizen.Web.Controllers
{
    //[AbpMvcAuthorize(PermissionNames.Pages_Addresses)]
    public class UserKinsController : KaizenControllerBase
    {
        private readonly IUserKinAppService _userKinService;
        private readonly KaizenDbContext _context;

        public UserKinsController(IUserKinAppService userKinService, KaizenDbContext context)
        {
            _userKinService = userKinService;
            _context = context;
        }

        public async Task<ActionResult> EditModal(long Id)
        {
            var userKin = await _userKinService.GetAsync(new EntityDto<long>(Id));

            var model = new EditUserKinModalViewModel
            {
                UserKin = userKin
            };

            return PartialView("_EditModalUserKin", model);
        }
    }
}