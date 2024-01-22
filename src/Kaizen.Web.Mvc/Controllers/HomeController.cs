using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Security;
using Kaizen.Controllers;
using Kaizen.EntityFrameworkCore;
using Kaizen.Web.Models.Home;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Kaizen.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : KaizenControllerBase
    {
        private readonly KaizenDbContext _context;

        public HomeController(KaizenDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var model = new HomeViewModel();

            if(PermissionChecker.IsGranted("User.SessionManagement.ViewHostedSessions"))
            {
                model.RecentSupportSessions = _context.SupportSession.Where(a => a.SupportProfessionalUserId == User.Identity.GetUserId().Value).ToList();
            }

            return View(model);
        }

        private void GenerateAppService(string entityType, string entityTypeCamel, string path)
        {
            using (FileStream fs = System.IO.File.Create(path + entityType + "/" + entityType + "AppService.cs"))
            {
                string pageContent = @"
            using System;
            using System.Linq;
            using Abp.Application.Services;
            using Abp.Domain.Repositories;
            using Abp.Extensions;
            using System.Linq.Dynamic.Core;
            using Kaizen.Entities.{{entityType}}s.Dto;


            namespace Kaizen.Entities.{{entityType}}s
            {
                public class {{entityType}}AppService : AsyncCrudAppService<{{entityType}}, {{entityType}}Dto, long, Paged{{entityType}}ResultRequestDto, Create{{entityType}}Dto, {{entityType}}Dto>, I{{entityType}}AppService
                {
                    public {{entityType}}AppService(
                        IRepository<{{entityType}}, long> repository)
                        : base(repository)
                    {
                    }

                    protected override IQueryable<{{entityType}}> ApplySorting(IQueryable<{{entityType}}> query, Paged{{entityType}}ResultRequestDto input)
                    {
                        if (!input.Sorting.IsNullOrWhiteSpace())
                        {
                            return query.OrderBy(input.Sorting);
                        }

                        return base.ApplySorting(query, input);
                    }

                    protected override IQueryable<{{entityType}}> CreateFilteredQuery(Paged{{entityType}}ResultRequestDto input)
                    {
                        var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

                        return Repository.GetAllIncluding();
                    }
                }
            }"
                ;

                try
                {
                    pageContent = pageContent.Replace("{{entityType}}", entityType);

                    byte[] info = new UTF8Encoding(true).GetBytes(pageContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                catch (Exception ex)
                {

                }
            }

            using (FileStream fs = System.IO.File.Create(path + entityType + "/" + "I" + entityType + "AppService.cs"))
            {
                string pageContent = @"
            using Abp.Application.Services;
            using Kaizen.Entities.{{entityType}}s.Dto;

            namespace Kaizen.Entities.{{entityType}}s
            {
                public interface I{{entityType}}AppService : IAsyncCrudAppService<{{entityType}}Dto, long, Paged{{entityType}}ResultRequestDto, Create{{entityType}}Dto, {{entityType}}Dto>
                {

                }
            }";

                try
                {
                    pageContent = pageContent.Replace("{{entityType}}", entityType);

                    byte[] info = new UTF8Encoding(true).GetBytes(pageContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void GenerateDtos(string entityType, string entityTypeCamel, string path)
        {
            using (FileStream fs = System.IO.File.Create(path + entityType + "/Dto/" + entityType + "Dto.cs"))
            {
                string pageContent = @"
            using System;
            using System.ComponentModel.DataAnnotations;
            using Abp.Application.Services.Dto;
            using Abp.AutoMapper;

            namespace Kaizen.Entities.{{entityType}}s.Dto
            {
                [AutoMapTo(typeof({{entityType}}))]
                public class {{entityType}}Dto : EntityDto<long>
                {

                }
            }";

                try
                {
                    pageContent = pageContent.Replace("{{entityType}}", entityType);

                    byte[] info = new UTF8Encoding(true).GetBytes(pageContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                catch (Exception ex)
                {

                }
            }


            using (FileStream fs = System.IO.File.Create(path + entityType + "/Dto/Create" + entityType + "Dto.cs"))
            {
                string pageContent = @"
            using System;
            using System.ComponentModel.DataAnnotations;
            using Abp.Application.Services.Dto;
            using Abp.AutoMapper;

            namespace Kaizen.Entities.{{entityType}}s.Dto
            {
                [AutoMapTo(typeof({{entityType}}))]
                public class Create{{entityType}}Dto : EntityDto<long>
                {

                }
            }";

                try
                {
                    pageContent = pageContent.Replace("{{entityType}}", entityType);

                    byte[] info = new UTF8Encoding(true).GetBytes(pageContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                catch (Exception ex)
                {

                }
            }


            using (FileStream fs = System.IO.File.Create(path + entityType + "/Dto/" + entityType + "MapProfile.cs"))
            {
                string pageContent = @"
            using AutoMapper;
            using Kaizen.Entities.{{entityType}}s;
            using Kaizen.Entities.{{entityType}}s.Dto;

            namespace Kaizen.Users.Dto
            {
                public class {{entityType}}MapProfile : Profile
                {
                    public {{entityType}}MapProfile()
                    {
                        CreateMap<{{entityType}}, {{entityType}}Dto>();
                        CreateMap<{{entityType}}, Create{{entityType}}Dto>();

                        CreateMap<Create{{entityType}}Dto, {{entityType}}>();
                    }
                }
            }";

                try
                {
                    pageContent = pageContent.Replace("{{entityType}}", entityType);

                    byte[] info = new UTF8Encoding(true).GetBytes(pageContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                catch (Exception ex)
                {

                }
            }

            using (FileStream fs = System.IO.File.Create(path + entityType + "/Dto/Paged" + entityType + "ResultRequestDto.cs"))
            {
                string pageContent = @"
            using System;
            using Abp.Application.Services.Dto;

            namespace Kaizen.Entities.{{entityType}}s.Dto
            {
                public class Paged{{entityType}}ResultRequestDto : PagedResultRequestDto
                {
                    public string Keyword { get; set; }
                    public string Sorting { get; set; }
                }
            }";

                try
                {
                    pageContent = pageContent.Replace("{{entityType}}", entityType);

                    byte[] info = new UTF8Encoding(true).GetBytes(pageContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                catch (Exception ex)
                {

                }
            }

        }

        private void GenerateControllers(string entityType, string entityTypeCamel, string path, string controllerPath)
        {
            using (FileStream fs = System.IO.File.Create(controllerPath + entityType + "sController.cs"))
            {
                string pageContent = @"
            using System;
            using System.Threading.Tasks;
            using Microsoft.AspNetCore.Mvc;
            using Kaizen.Controllers;
            using Kaizen.Entities.{{entityType}}s;
            using Kaizen.Web.Models.{{entityType}}s;
            using Abp.Application.Services.Dto;

            namespace Kaizen.Web.Areas.Controllers
            {
                public class {{entityType}}sController : KaizenControllerBase
                {
                    private readonly I{{entityType}}AppService _{{entityTypeCamel}}AppService;

                    public {{entityType}}sController(I{{entityType}}AppService {{entityTypeCamel}}AppService)
                    {
                        _{{entityTypeCamel}}AppService = {{entityTypeCamel}}AppService;
                    }

                    public async Task<ActionResult> Index()
                    {
                        var model = new {{entityType}}ListViewModel
                        {
               
                        };
                        return View(model);
                    }

                    public async Task<ActionResult> Edit(long? id)
                    {
                        if(id == null)
                        {
                            return NotFound();
                        }

                        var {{entityTypeCamel}} = await _{{entityTypeCamel}}AppService.GetAsync(new EntityDto<long>(id.Value));
                        var model = new {{entityType}}EditViewModel
                        {
                            {{entityType}} = {{entityTypeCamel}}
                        };

                        return View(model);
                    }

                    [HttpPost]
                    public async Task<ActionResult> Edit({{entityType}}EditViewModel model)
                    {
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                await _{{entityTypeCamel}}AppService.UpdateAsync(model.{{entityType}});
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                        return View(model);
                    }


                    public async Task<ActionResult> EditModal(long {{entityTypeCamel}}Id)
                    {
                        Entities.{{entityType}}s.Dto.{{entityType}}Dto {{entityTypeCamel}} = await _{{entityTypeCamel}}AppService.GetAsync(new EntityDto<long>({{entityTypeCamel}}Id));
                        var model = new Edit{{entityType}}ModalViewModel
                        {
                            {{entityType}} = {{entityTypeCamel}}
                        };
                        return PartialView(""_EditModal"", model);
                    }
                }
            }";

                try
                {
                    pageContent = pageContent.Replace("{{entityType}}", entityType).Replace("{{entityTypeCamel}}", entityTypeCamel);

                    byte[] info = new UTF8Encoding(true).GetBytes(pageContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void GenerateViewModels(string entityType, string entityTypeCamel, string modelPath, string viewPath)
        {
            using (FileStream fs = System.IO.File.Create(modelPath + entityType + "/" + entityType + "ListViewModel.cs"))
            {
                string pageContent = @"
            using System.Collections.Generic;
            using Kaizen.Entities.{{entityType}}s.Dto;

            namespace Kaizen.Web.Models.{{entityType}}s
            {
                public class {{entityType}}ListViewModel
                {
                    public IReadOnlyList<{{entityType}}Dto> Entities { get; set; }
                }
            }";

                try
                {
                    pageContent = pageContent.Replace("{{entityType}}", entityType).Replace("{{entityTypeCamel}}", entityTypeCamel);

                    byte[] info = new UTF8Encoding(true).GetBytes(pageContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                catch (Exception ex)
                {

                }
            }

            using (FileStream fs = System.IO.File.Create(modelPath + entityType + "/" + entityType + "EditViewModel.cs"))
            {
                string pageContent = @"
            using System.Collections.Generic;
            using Kaizen.Entities.{{entityType}}s.Dto;

            namespace Kaizen.Web.Models.{{entityType}}s
            {
                public class {{entityType}}EditViewModel
                {
                     public {{entityType}}Dto {{entityType}} { get; set; }
                }
            }";

                try
                {
                    pageContent = pageContent.Replace("{{entityType}}", entityType).Replace("{{entityTypeCamel}}", entityTypeCamel);

                    byte[] info = new UTF8Encoding(true).GetBytes(pageContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                catch (Exception ex)
                {

                }
            }

            using (FileStream fs = System.IO.File.Create(modelPath + entityType + "/Edit" + entityType + "ModalViewModel.cs"))
            {
                string pageContent = @"
            using System.Collections.Generic;
            using Kaizen.Entities.{{entityType}}s.Dto;

            namespace Kaizen.Web.Models.{{entityType}}s
            {
                public class Edit{{entityType}}ModalViewModel
                {
                    public {{entityType}}Dto {{entityType}} { get; set; }
                }
            }";

                try
                {
                    pageContent = pageContent.Replace("{{entityType}}", entityType).Replace("{{entityTypeCamel}}", entityTypeCamel);

                    byte[] info = new UTF8Encoding(true).GetBytes(pageContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                catch (Exception ex)
                {

                }
            }

        }

        private void GenerateViews(string entityType, string entityTypeCamel, string path, string viewPath)
        {
            using (FileStream fs = System.IO.File.Create(viewPath + entityType + "/" + "Index.cshtml"))
            {
                string pageContent = @$"@using Kaizen.Web.Startup
                @using Kaizen.Web.Models.{entityType}s
                @model {entityType}ListViewModel
                @{{
                    ViewBag.Title = L(""{entityType}"");
                    ViewBag.CurrentPageName = PageNames.Customers;
                }}
                @section scripts
                {{
                    <environment names=""Development"">
                        <script src=""~/view-resources/Views/{entityType}/Index.js"" asp-append-version=""true""></script>
                    </environment>

                    <environment names=""Staging,Production"">
                        <script src=""~/view-resources/Views/{entityType}/Index.min.js"" asp-append-version=""true""></script>
                    </environment>
                }}
                <section class=""content-header"">
                    <div class=""container-fluid"">
                        <div class=""row"">
                            <div class=""col-12"">
                                <div class=""page-title-box d-flex align-items-center justify-content-between"">
                                    <h4 class=""mb-0"">@L(""{entityType}s"")</h4>

                                    <div class=""page-title-right"">
                                        <a href=""javascript:;"" data-bs-toggle=""modal"" data-bs-target=""#{entityType}CreateModal"" class=""btn btn-primary"">
                                            <i class=""fa fa-plus-square""></i>
                                            @L(""Create"")
                                        </a>
                                    </div>
                                </div>
                
                            </div>
                        </div>
                    </div>
                </section>

                <div class=""container-fluid"">
                    <div class=""row"">
                        <div class=""col-12"">
                            <div class=""card"">
                                <div class=""card-header"">
                                    <div class=""row"">
                                        <div class=""col-md-6"">
                                            <!--Use for bulk actions-->
                                        </div>
                                        <div class=""col-md-6"">
                                            @await Html.PartialAsync(""~/Views/{entityType}s/Index.AdvancedSearch.cshtml"")
                                        </div>
                                    </div>
                                </div>
                                <div class=""card-body"">
                                    <div class=""table-responsive"">
                                        <table id=""{entityType}Table"" class=""table table-hover table-nowrap mb-0 align-middle table-check"">
                                            <thead class=""table-light"">
                                                <tr>
                                                    <th class=""rounded-start""></th>
              
                                                    <th class=""no-filter rounded-end"" style=""width: 100px"">@L(""Actions"")</th>
                                                </tr>
                                            </thead>
                                            <tbody></tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                @await Html.PartialAsync(""~/Views/{entityType}s/_CreateModal.cshtml"", Model)

                <div class=""modal fade"" id=""{entityType}EditModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""{entityType}EditModalLabel"" data-backdrop=""static"">
                    <div class=""modal-dialog modal-lg"" role=""document"">
                        <div class=""modal-content"">
                        </div>
                    </div>
                </div>";

                try
                {
                    pageContent = pageContent.Replace("{{entityType}}", entityType).Replace("{{entityTypeCamel}}", entityTypeCamel);

                    byte[] info = new UTF8Encoding(true).GetBytes(pageContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                catch (Exception ex)
                {

                }
            }

            using (FileStream fs = System.IO.File.Create(viewPath + entityType + "/" + "Index.AdvancedSearch.cshtml"))
            {
                string pageContent = @$"<div class=""abp-advanced-search"">
                    <form id=""{entityType}SearchForm"" class=""form-horizontal"">
                        <div class=""input-group"">
                            <div class=""input-group-prepend"">
                                <button type=""button"" class=""btn bg-blue btn-search"">
                                    <span class=""fas fa-search"" aria-hidden=""true""></span>
                                </button>
                            </div>
                            <input type=""text"" name=""Keyword"" class=""form-control txt-search"" />
                        </div>
                    </form>
                </div>
                ";

                try
                {
                    pageContent = pageContent.Replace("{{entityType}}", entityType).Replace("{{entityTypeCamel}}", entityTypeCamel);

                    byte[] info = new UTF8Encoding(true).GetBytes(pageContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                catch (Exception ex)
                {

                }
            }

            using (FileStream fs = System.IO.File.Create(viewPath + entityType + "/" + "_EditModal.cshtml"))
            {
                string pageContent = @"@using Abp.Authorization.Users
                @using Kaizen.Web.Models.Common.Modals
                @using Kaizen.Web.Models." + entityType + "s" +
                "" +
                "@model Edit" + entityType + "ModalViewModel" +
                @" 
                " +
                "@{" +
                    "Layout = null;" +
                "}" +
                @" 
                " +
                @$"@await Html.PartialAsync(""~/Views/Shared/Modals/_ModalHeader.cshtml"", new ModalHeaderViewModel(L(""Edit{entityType}"")))" +
                @$"<form name=""{entityTypeCamel}EditForm"" role=""form"" class=""form-horizontal"">
                    <input type=""hidden"" name=""Id"" value=""@Model.{entityType}.Id"" />
                    <div class=""modal-body"">
                         <div class=""mb-3 row required"">
                            <label class=""col-md-3 col-form-label"">@L(""Name"")</label>
                            <div class=""col-md-9"">
                                <input type=""text"" class=""form-control"" name=""Name"" value=""@Model.{entityType}.Name"" required minlength=""2"">
                            </div>
                        </div>
                    </div>" +
                @$"@await Html.PartialAsync(""~/Views/Shared/Modals/_ModalFooterWithSaveAndCancel.cshtml"")
                </form>

                <script src = ""~/view-resources/Views/{entityType}/_EditModal.js"" asp-append-version=""true""></script>";

                try
                {
                    pageContent = pageContent.Replace("{{entityType}}", entityType).Replace("{{entityTypeCamel}}", entityTypeCamel);

                    byte[] info = new UTF8Encoding(true).GetBytes(pageContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                catch (Exception ex)
                {

                }
            }

            using (FileStream fs = System.IO.File.Create(viewPath + entityType + "/" + "_CreateModal.cshtml"))
            {
                string pageContent = @"@using Abp.Authorization.Users
                @using Kaizen.Web.Models.Common.Modals
                @using Kaizen.Web.Models." + entityType + "s" +
                @"
                @model " + entityType + "ListViewModel" +
                @" 
                " +
                "@{" +
                    "Layout = null;" +
                "}" +
                @" 
                " +
                $"<div class=\"modal fade\" id=\"{entityType}CreateModal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"{entityType}CreateModalLabel\" data-backdrop=\"static\">\n    <div class=\"modal-dialog modal-lg\" role=\"document\">\n        <div class=\"modal-content\">" +

                @$"@await Html.PartialAsync(""~/Views/Shared/Modals/_ModalHeader.cshtml"", new ModalHeaderViewModel(L(""Create{entityType}"")))" +
                @$"<form name=""{entityTypeCamel}CreateForm"" role=""form"" class=""form-horizontal"">
                    <div class=""modal-body"">
                        <div class=""mb-3 row required"">
                            <label class=""col-md-3 col-form-label"">@L(""Name"")</label>
                            <div class=""col-md-9"">
                                <input type=""text"" class=""form-control"" name=""Name"" required minlength=""2"">
                            </div>
                        </div>
                    </div>" +
                @"@await Html.PartialAsync(""~/Views/Shared/Modals/_ModalFooterWithSaveAndCancel.cshtml"")
                </form></div></div></div>";

                try
                {
                    pageContent = pageContent.Replace("{{entityType}}", entityType).Replace("{{entityTypeCamel}}", entityTypeCamel);

                    byte[] info = new UTF8Encoding(true).GetBytes(pageContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void GenerateJavaScript(string entityType, string entityTypeCamel, string path)
        {
            using (FileStream fs = System.IO.File.Create(path + entityType + "/Index.js"))
            {
                string pageContent = @$"class {entityType} {{

                        constructor() {{
                            this._{entityTypeCamel}Service = abp.services.app.{entityTypeCamel};
                            this._${entityTypeCamel}Modal = $('#{entityType}CreateModal');
                            this._${entityTypeCamel}Form = this._${entityTypeCamel}Modal.find('form');
                            this._$table = $('#{entityType}Table');
                            this.l = abp.localization.getSource('Kaizen');
                        }}

                        setupGrid() {{
                            let self = this;

                            this._${entityTypeCamel}Table = this._$table.DataTable({{
                                paging: true,
                                serverSide: true,
                                ordering: true,
                                listAction: {{
                                    ajaxFunction: this._{entityTypeCamel}Service.getAll,
                                    inputFilter: function () {{
                                        var filter = $('#{entityType}SearchForm').serializeFormToObject(true);

                                        return filter;
                                    }}
                                }},
                                buttons: [
                                    {{
                                        name: 'refresh',
                                        text: '<i class=""fas fa-redo-alt""></i>',
                                        action: () => this._${entityTypeCamel}Table.draw(false)
                                    }}
                                ],
                                responsive: {{
                                    details: {{
                                        type: 'column'
                                    }}
                                }},
                                columnDefs: [
                                    {{
                                        targets: 0,
                                        className: 'control',
                                        defaultContent: '',
                                    }},
                                    {{
                                        targets: 1,
                                        data: 'name'
                                    }},
                                    {{
                                        targets: 2,
                                        data: null,
                                        sortable: false,
                                        autoWidth: false,
                                        defaultContent: '',
                                        render: (data, type, row, meta) => {{
                                            return [
                                                `<div class=""dropdown"">`,
                                                `    <button class=""btn btn-light btn-sm dropdown-toggle"" type=""button"" data-bs-toggle=""dropdown"" aria-expanded=""false"" data-popper-placement=""right-end"">`,
                                                `        <i class=""fas fa-ellipsis-h""></i>`,
                                                `    </button>`,
                                                `    <ul class=""dropdown-menu dropdown-menu-end"">`,
                                                `        <li><a class=""dropdown-item edit-{entityTypeCamel}"" data-{entityTypeCamel}-id=""${{row.id}}"" data-bs-toggle=""modal"" data-bs-target=""#{entityType}EditModal"">${{self.l('Edit')}}</a></li>`,
                                                `        <li><a class=""dropdown-item delete-{entityTypeCamel}"" href=""#"" data-{entityTypeCamel}-id=""${{row.id}}"" data-{entityTypeCamel}=""${{row.name}}"">${{self.l('Delete')}}</a></li>`,
                                                `    </ul>`,
                                                `</div>`
                                            ].join('');
                                        }}
                                    }}
                                ],
                            }});

                            this._${entityTypeCamel}Form.find('.save-button').on('click', (e) => {{
                                e.preventDefault();

                                if (!this._${entityTypeCamel}Form.valid()) {{
                                    return;
                                }}

                                var {entityTypeCamel} = this._${entityTypeCamel}Form.serializeFormToObject();

                                abp.ui.setBusy(this._${entityTypeCamel}Modal);

                                this._{entityTypeCamel}Service.create({entityTypeCamel}).done(function () {{
                                    self._${entityTypeCamel}Modal.modal('hide');
                                    self._${entityTypeCamel}Form[0].reset();
                                    abp.notify.info(self.l('SavedSuccessfully'));
                                    self._${entityTypeCamel}Table.ajax.reload();
                                }}).always(function () {{
                                    abp.ui.clearBusy(self._${entityTypeCamel}Modal);
                                }});
                            }});

                            $(document).on('click', '.delete-{entityTypeCamel}', function () {{
                                var {entityTypeCamel}id = $(this).attr(""data-{entityTypeCamel}-id"");

                                delete{entityType}({entityTypeCamel}id);
                            }});

                            $(document).on('click', '.edit-{entityTypeCamel}', function (e) {{
                                var {entityTypeCamel}Id = $(this).attr(""data-{entityTypeCamel}-id"");

                                e.preventDefault();
                                abp.ajax({{
                                    url: abp.appPath + '{entityType}s/EditModal?{entityTypeCamel}Id=' + {entityTypeCamel}Id,
                                    type: 'POST',
                                    dataType: 'html',
                                    success: function (content) {{
                                        $('#{entityType}EditModal div.modal-content').html(content);
                                    }},
                                    error: function (e) {{
                                    }}
                                }});
                            }});

                            $(document).on('click', 'a[data-bs-target=""#{entityType}CreateModal""]', (e) => {{
                                $('.nav-tabs a[href=""#{entityTypeCamel}-details""]').tab('show')
                            }});

                            abp.event.on('{entityTypeCamel}.edited', (data) => {{
                                self._${entityTypeCamel}Table.ajax.reload();
                            }});

                            this._${entityTypeCamel}Modal.on('shown.bs.modal', () => {{
                                self._${entityTypeCamel}Modal.find('input:not([type=hidden]):first').focus();
                            }}).on('hidden.bs.modal', () => {{
                                self._${entityTypeCamel}Form.clearForm();
                            }});

                            $('.btn-search').on('click', (e) => {{
                                self._${entityTypeCamel}Table.ajax.reload();
                            }});

                            $('.txt-search').on('keypress', (e) => {{
                                if (e.which == 13) {{
                                    self._${entityTypeCamel}Table.ajax.reload();
                                   return false;
                                }}
                            }});
                        }}

                        delete{entityType}({entityTypeCamel}id) {{
                            abp.message.confirm(
                                abp.utils.formatString(
                                    self.l('AreYouSureWantToDelete'),
                                    userName),
                                null,
                                (isConfirmed) => {{
                                    if (isConfirmed) {{
                                        self._{entityTypeCamel}service.delete({{
                                            id: {entityTypeCamel}id
                                        }}).done(() => {{
                                            abp.notify.info(self.l('SuccessfullyDeleted'));
                                            self._${entityTypeCamel}Table.ajax.reload();
                                        }});
                                    }}
                                }}
                            );
                        }}
                    }}
   
                ";

                try
                {
                    pageContent = pageContent.Replace("{{entityType}}", entityType).Replace("{{entityTypeCamel}}", entityTypeCamel);

                    byte[] info = new UTF8Encoding(true).GetBytes(pageContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                catch (Exception ex)
                {

                }
            }

            using (FileStream fs = System.IO.File.Create(path + entityType + "/_EditModal.js"))
            {
                string pageContent = @$"(function ($) {{
                                        var _{entityTypeCamel}Service = abp.services.app.{entityTypeCamel},
                                            l = abp.localization.getSource('Kaizen'),
                                            _$modal = $('#{entityType}EditModal'),
                                            _$form = _$modal.find('form');

                                        function save() {{
                                            if (!_$form.valid()) {{
                                                return;
                                            }}

                                            var {entityTypeCamel} = _$form.serializeFormToObject();

                                            abp.ui.setBusy(_$form);
                                            _{entityTypeCamel}Service.update({entityTypeCamel}).done(function () {{
                                                _$modal.modal('hide');
                                                abp.notify.info(l('SavedSuccessfully'));
                                                abp.event.trigger('{entityTypeCamel}.edited', {entityTypeCamel});
                                            }}).always(function () {{
                                                abp.ui.clearBusy(_$form);
                                            }});
                                        }}

                                        _$form.closest('div.modal-content').find("".save-button"").click(function (e) {{
                                            e.preventDefault();
                                            save();
                                        }});

                                        _$form.find('input').on('keypress', function (e) {{
                                            if (e.which === 13) {{
                                                e.preventDefault();
                                                save();
                                            }}
                                        }});

                                        _$modal.on('shown.bs.modal', function () {{
                                            _$form.find('input[type=text]:first').focus();
                                        }});
                                    }})(jQuery);";

                try
                {
                    pageContent = pageContent.Replace("{{entityType}}", entityType).Replace("{{entityTypeCamel}}", entityTypeCamel);

                    byte[] info = new UTF8Encoding(true).GetBytes(pageContent);

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void GenerateCode()
        {
            /*  "CustomerSession", 
                "NotificationTemplate", 
                "Timesheet", 
                "Invoice"
            */

            //string[] entityList = {
            //    "University",
            //    "Disability",
            //    "ContactMethod",
            //    "StorageFile",
            //    "Region",
            //    "RegionLocation",
            //    "CustomerType",
            //    "PaymentTerm"
            //};

            string[] entityList = {
                "Checklist",
                "ChecklistItem"
            };


            foreach (String entityType in entityList)
            {
                // Create files
                string entityTypeCamel = entityType.Substring(0, 1).ToLower() + entityType.Substring(1);
                string path = @"c:\TestData\Entities\";
                string controllerPath = @"c:\TestData\Controllers\";
                string modelPath = @"c:\TestData\Models\";
                string viewPath = @"c:\TestData\Views\";
                string jsPath = @"c:\TestData\view-resources\Views\";

                System.IO.Directory.CreateDirectory(path + entityType);
                System.IO.Directory.CreateDirectory(path + entityType + "/Dto");
                System.IO.Directory.CreateDirectory(controllerPath + entityType);
                System.IO.Directory.CreateDirectory(modelPath + entityType);
                System.IO.Directory.CreateDirectory(viewPath + entityType);
                System.IO.Directory.CreateDirectory(jsPath + entityType);

                GenerateAppService(entityType, entityTypeCamel, path);

                GenerateDtos(entityType, entityTypeCamel, path);

                GenerateControllers(entityType, entityTypeCamel, path, controllerPath);

                GenerateViewModels(entityType, entityTypeCamel, modelPath, viewPath);

                GenerateViews(entityType, entityTypeCamel, path, viewPath);

                GenerateJavaScript(entityType, entityTypeCamel, jsPath);
            }
        }
    }
}
