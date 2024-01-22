using Abp.Application.Services.Dto;
using Kaizen.Controllers;
using Kaizen.Entities.Addresses;
using Kaizen.EntityFrameworkCore;
using Kaizen.Web.Models.Addresses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kaizen.Web.Controllers
{
    //[AbpMvcAuthorize(PermissionNames.Pages_Addresses)]
    public class AddressController : KaizenControllerBase
    {
        private readonly IAddressAppService _addressAppService;
        private readonly KaizenDbContext _context;

        public AddressController(IAddressAppService addressAppService, KaizenDbContext context)
        {
            _addressAppService = addressAppService;
            _context = context;
        }

        public async Task<ActionResult> EditModal(long addressId)
        {
            var address = await _addressAppService.GetAsync(new EntityDto<long>(addressId));

            var model = new EditAddressModalViewModel
            {
                Address = address
            };

            return PartialView("_EditModal", model);
        }
    }
}
