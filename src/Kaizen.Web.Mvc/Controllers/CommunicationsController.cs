using Abp.Application.Services.Dto;
using Kaizen.Controllers;
using Kaizen.Entities.Addresses;
using Kaizen.Entities.CommunicationGroups;
using Kaizen.Entities.CommunicationGroups.Dto;
using Kaizen.EntityFrameworkCore;
using Kaizen.Enums;
using Kaizen.Web.Models.Addresses;
using Kaizen.Web.Models.Communication;
using Kaizen.Web.Models.CommunicationGroup;
using Kaizen.Web.Models.CommunicationTemplate;
using Kaizen.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Kaizen.Web.Areas.Controllers
{
    //[AbpMvcAuthorize(PermissionNames.Pages_Addresses)]
    public class CommunicationsController : KaizenControllerBase
    {
        private readonly ICommunicationGroupAppService _communicationGroupAppService;
        private readonly KaizenDbContext _context;

        public CommunicationsController(ICommunicationGroupAppService communicationGroupAppService, KaizenDbContext context)
        {
            _communicationGroupAppService = communicationGroupAppService;
            _context = context;
        }

        public async Task<ActionResult> Groups()
        {
            var communicationGroups = (await _communicationGroupAppService.GetAllAsync(new Entities.CommunicationGroups.Dto.PagedCommunicationGroupRequestDto { })).Items;
            var model = new CommunicationGroupListViewModel
            {
                Entities = communicationGroups,
                Users = _context.Users.Where(a => !a.IsDeleted).Select(a => new SelectListItem
                {
                    Text = a.FullName,
                    Value = a.Id.ToString()
                }).ToList()
            };
            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            var model = new CreateCommunicationViewModel
            {

            };

            PopulateViewModel(model);

            return View(model);
        }

        public async Task<ActionResult> GroupsEditModal(long communicationGroupId)
        {
            var communicationGroup = ObjectMapper.Map<CommunicationGroupDto>(_context.CommunicationGroup.Include(a => a.CommunicationGroupUsers).SingleOrDefault(a => a.Id == communicationGroupId));

            communicationGroup.UserList = communicationGroup.CommunicationGroupUsers.Select(a => a.UserId).ToList();
            
            var model = new EditCommunicationGroupModalViewModel
            {
                CommunicationGroup = communicationGroup,
                Users = _context.Users.Where(a => !a.IsDeleted).Select(a => new SelectListItem
                {
                    Text = a.FullName,
                    Value = a.Id.ToString()
                }).ToList()
            };

            return PartialView("/Views/CommunicationGroups/_EditModal.cshtml", model);
        }

        private void PopulateViewModel(CommunicationTemplateBaseViewModel model)
        {
            model.ContentTemplates = _context.CommunicationTemplate.Where(a => a.TemplateArea == CommunicationTemplateArea.Content).Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToList();

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
