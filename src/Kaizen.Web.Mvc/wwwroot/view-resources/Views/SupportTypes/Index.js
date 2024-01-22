class SupportType {

    constructor() {
        this._supportTypeService = abp.services.app.supportType;
        this._$modal = $('#SupportTypeCreateModal');
        this._$form = this._$modal.find('form');
        this._$table = $('#SupportTypesTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        var self = this;

        var _$supportTypesTable = self._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: self._supportTypeService.getAll,
                inputFilter: function () {
                    var filter = $('#supportTypesSearchForm').serializeFormToObject(true);

                    //filter.role = role;

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => self._$supportTypesTable.draw(false)
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
                    data: 'cost',
                    sortable: false
                },
                {
                    targets: 4,
                    data: 'margin'
                },
                {
                    targets: 5,
                    data: 'sellPrice'
                },
                {
                    targets: 6,
                    data: null,
                    sortable: false,
                    autoWidth: false,
                    defaultContent: '',
                    render: (data, type, row, meta) => {
                        return [
                            `<div class="dropdown">`,
                            `    <button class="btn btn-light btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false" data-popper-placement="right-end">`,
                            `        <i class="fas fa-ellipsis-h"></i>`,
                            `    </button>`,
                            `    <ul class="dropdown-menu dropdown-menu-end">`,
                            `        <li><a class="dropdown-item edit-support-type" data-support-type-id="${row.id}" data-bs-toggle="modal" data-bs-target="#SupportTypeEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-support-type" href="#" data-support-type-id="${row.id}" data-support-type="${row.name}">${self.l('Delete')}</a></li>`,
                            `    </ul>`,
                            `</div>`
                        ].join('');
                    }
                }
            ]
        });

        self.setupCreate();
    }

    setupCreate() { 
        var self = this;

        self._$form.find('.save-button').off();
        self._$form.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var supportType = self._$form.serializeFormToObject();

            abp.ui.setBusy(self._$modal);
            self._supportTypeService.create(supportType).done(function () {
                self._$modal.modal('hide');
                self._$form[0].reset();
                abp.notify.info(l('SavedSuccessfully'));
                self._$supportTypesTable.ajax.reload();
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });

        $(document).on('click', '.delete-user', function () {
            var supportTypeId = $(this).attr("data-support-type-id");
            var supportName = $(this).attr("data-support-type");

            deleteSupportType(supportTypeId, supportName);
        });

        function deleteSupportType(supportTypeId, userName) {
            abp.message.confirm(
                abp.utils.formatString(
                    l('AreYouSureWantToDelete'),
                    userName),
                null,
                (isConfirmed) => {
                    if (isConfirmed) {
                        self._supportTypeService.delete({
                            id: supportTypeId
                        }).done(() => {
                            abp.notify.info(l('SuccessfullyDeleted'));
                            self._$supportTypesTable.ajax.reload();
                        });
                    }
                }
            );
        }

        $(document).on('click', '.edit-support-type', function (e) {
            var supportTypeId = $(this).attr("data-support-type-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'SupportTypes/EditModal?supportTypeId=' + supportTypeId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#SupportTypeEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        abp.event.on('supportType.edited', (data) => {
            self._$supportTypesTable.ajax.reload();
        });

        self._$modal.on('shown.bs.modal', () => {
            self._$modal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$form.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$supportTypesTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$supportTypesTable.ajax.reload();
                return false;
            }
        });
    }
}
