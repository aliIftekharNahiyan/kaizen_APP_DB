
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Security;
using Azure;
using Kaizen.Controllers;
using Kaizen.Entities.Lookups;
using Kaizen.Entities.Lookups.Dto;
using Kaizen.Web.Models.Lookups;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Kaizen.Web.Areas.Controllers
{
    public class LookupsController : KaizenControllerBase
    {
        private readonly ILookupAppService _lookupAppService;

        public LookupsController(ILookupAppService lookupAppService)
        {
            _lookupAppService = lookupAppService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new LookupListViewModel
            {

            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateLookupDto model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                try
                {
                    //  Duplicate Check

                    //var lookUp = _lookupAppService.GetManyQueryable(w => w.Name.Trim().Equals(model.Name.Trim()) && w.LookTypeId == model.LookTypeId && !w.IsDeleted); ;
                    //if (lookUp.Any())
                    //{
                    //    return BadRequest("Lookup exist!!");
                    //}
                    model.CreatedBy = User.Identity.GetUserId().Value;
                    model.CreatedDate = DateTime.Now;
                    model.IsDeleted = false;

                    await _lookupAppService.CreateAsync(model);
                }
                catch (RequestFailedException ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            catch (Exception ex)
            {
                return BadRequest("File is required");

            }

            return Ok(model);
        }
        
        [HttpGet]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Lookup = await _lookupAppService.GetAsync(new EntityDto<long>(id.Value));
            var model = new LookupEditViewModel
            {
                Lookup = Lookup
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromBody]  EditLookupModalViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //  Need Duplicate Check

                    //var lookUp = _lookupAppService.GetManyQueryable(w => w.Name.Trim().Equals(model.Lookup.Name.Trim()) && w.LookTypeId == model.Lookup.LookTypeId && !w.IsDeleted); ;
                    //if (lookUp.Any())
                    //{
                    //    return BadRequest("Lookup name exist!!");
                    //}
                    model.Lookup.UpdatedDate = DateTime.Now;
                    model.Lookup.UpdatedBy = User.Identity.GetUserId().Value;

                   var result = await _lookupAppService.UpdateAsync(model.Lookup);
                }
                catch (Exception ex)
                {

                }
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(long id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    LookupDto lookup = await _lookupAppService.GetAsync(new EntityDto<long>(id));

                    if (lookup.Id == null)
                    {
                        return BadRequest("Lookup not exist!!");
                    }
                    lookup.UpdatedDate = DateTime.Now;
                    lookup.UpdatedBy = User.Identity.GetUserId().Value;
                    lookup.IsDeleted = true;
                    lookup.IsActive = false;

                    var dto = ObjectMapper.Map<LookupDto>(lookup);
                    await _lookupAppService.UpdateAsync(dto);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok("success");
        }


        public async Task<ActionResult> EditModal(long lookupId)
        {
            LookupDto lookup = await _lookupAppService.GetAsync(new EntityDto<long>(lookupId));
            var model = new EditLookupModalViewModel
            {
                Lookup = lookup
            };
            return PartialView("_EditModal", model);
        }
    }
}