using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Kaizen.Authorization;
using Kaizen.Controllers;
using Kaizen.MultiTenancy;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kaizen.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantsController : KaizenControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantsController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        public ActionResult Index() => View();

        public async Task<ActionResult> EditModal(int tenantId)
        {
            var tenantDto = await _tenantAppService.GetAsync(new EntityDto(tenantId));
            return PartialView("_EditModal", tenantDto);
        }
    }
}
