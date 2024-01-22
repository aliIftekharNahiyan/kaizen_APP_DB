class Company {

    constructor() {
        this._companyService = abp.services.app.company;
        this._storateFileService = abp.services.app.storageFile;

        this._$companyModal = $('#CompanyCreateModal');
        this._$companyForm = this._$companyModal.find('form');
        this._$table = $('#CompanyTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        this._$companyTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._companyService.getAll,
                inputFilter: function () {
                    var filter = $('#CompanySearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$companyTable.draw(false)
                }
            ],
            responsive: {
                details: {
                    type: 'column'
                }
            },
            columnDefs: [
                {
                    targets: 0,
                    className: 'control',
                    defaultContent: '',
                },
                {
                    targets: 1,
                    data: 'name'
                },

                {
                    targets: 2,
                    data: 'description'
                },
                {
                    targets: 3,
                    data: 'sendFromEmailAddress'
                },

                {
                    targets: 4,
                    data: 'primaryBrandColour'
                },
                {
                    targets: 5,
                    data: 'secondaryBrandColour'
                },
                {
                    targets: 6,
                    data: 'isActive'
                },

                {
                    targets: 7,
                    data: null,
                    sortable: false,
                    autoWidth: false,
                    defaultContent: '',
                    render: (data, type, row) => {
                        return [
                            `<div class="dropdown">`,
                            `    <button class="btn btn-light btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false" data-popper-placement="right-end">`,
                            `        <i class="fas fa-ellipsis-h"></i>`,
                            `    </button>`,
                            `    <ul class="dropdown-menu dropdown-menu-end">`,
                            `        <li><a class="dropdown-item edit-company" data-company-id="${row.id}" data-bs-toggle="modal" data-bs-target="#CompanyEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-company" href="#" data-company-id="${row.id}" data-company-logfileId="${row.logoFileId}" data-company="${row.name}">${self.l('Delete')}</a></li>`,
                            `    </ul>`,
                            `</div>`
                        ].join('');
                    }
                }
            ],
        });

        self.setupCreate();
    }

    setupCreate() {
        var self = this;

        this._$companyForm.find('.save-button').off();     

        $(document).on('click', '.delete-company', function () {
            var companyid = $(this).attr("data-company-id");            
            var storeId = $(this).attr("data-company-logfileId");
            self.deleteCompany(companyid, $(this).attr("data-company"), storeId);
        });

        $(document).on('click', '.edit-company', function (e) {
            var companyId = $(this).attr("data-company-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'Companies/EditModal?companyId=' + companyId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#CompanyEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#CompanyCreateModal"]', (e) => {
            $('.nav-tabs a[href="#company-details"]').tab('show')
        });

        abp.event.on('company.edited', (data) => {
            self._$companyTable.ajax.reload();
        });

        abp.event.on('company.created', (data) => {
            self._$companyTable.ajax.reload();
        });

        this._$companyModal.on('shown.bs.modal', () => {
            self._$companyModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$companyForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$companyTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$companyTable.ajax.reload();
                return false;
            }
        });
    }

    deleteCompany(companyid, userName,storageId) {
        let self = this;

        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._companyService.delete({
                        id: companyid
                    }).done(() => {
                        self._storateFileService.delete({ id: storageId }).done(() => { });
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$companyTable.ajax.reload();
                    });
                }
            }
        );
    }
        
}



