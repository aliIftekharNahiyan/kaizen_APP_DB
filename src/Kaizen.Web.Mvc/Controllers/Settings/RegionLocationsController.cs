
using Abp.Application.Services.Dto;
using AutoMapper;
using Kaizen.Controllers;
using Kaizen.Entities.RegionLocations;
using Kaizen.Entities.RegionLocations.Dto;
using Kaizen.Entities.Regions;
using Kaizen.EntityFrameworkCore;
using Kaizen.Web.Models.RegionLocations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Kaizen.Web.Areas.Controllers
{
    public class RegionLocationsController : KaizenControllerBase
    {
        private readonly IRegionLocationAppService _regionLocationAppService;
        private readonly IRegionAppService _regionAppService;
        private readonly KaizenDbContext _context;
        private IMapper _mapper;

        public RegionLocationsController(IRegionLocationAppService regionLocationAppService, IRegionAppService regionAppService, KaizenDbContext context, IMapper mapper)
        {
            _regionLocationAppService = regionLocationAppService;
            _regionAppService = regionAppService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var model = new RegionLocationListViewModel
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

            var regionLocation = await _regionLocationAppService.GetAsync(new EntityDto<long>(id.Value));
            var regions = (await _regionAppService.GetAllAsync(new Entities.Regions.Dto.PagedRegionResultRequestDto())).Items.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

            var model = new RegionLocationEditViewModel
            {
                RegionLocation = regionLocation,
                RegionList = regions
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RegionLocationEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _regionLocationAppService.UpdateAsync(model.RegionLocation);
                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
        }


        public async Task<ActionResult> EditModal(long regionLocationId)
        {
            var regionLocation = _mapper.Map<RegionLocationDto>(_context.RegionLocation.Include(a => a.Region).AsNoTracking().SingleOrDefault(a => a.Id == regionLocationId));

            var regions = (await _regionAppService.GetAllAsync(new Entities.Regions.Dto.PagedRegionResultRequestDto())).Items.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

            var model = new EditRegionLocationModalViewModel
            {
                RegionLocation = regionLocation,
                RegionList = regions
            };
            return PartialView("_EditModal", model);
        }
    }
}