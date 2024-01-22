class CustomerType {

    constructor() {
        this._customerTypeService = abp.services.app.customerType;
        this._$customerTypeModal = $('#CustomerTypeCreateModal');
        this._$customerTypeForm = this._$customerTypeModal.find('form');
        this._$table = $('#CustomerTypeTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        this._$customerTypeTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._customerTypeService.getAll,
                inputFilter: function () {
                    var filter = $('#CustomerTypeSearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$customerTypeTable.draw(false)
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
                            `        <li><a class="dropdown-item edit-customerType" data-customerType-id="${row.id}" data-bs-toggle="modal" data-bs-target="#CustomerTypeEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-customerType" href="#" data-customerType-id="${row.id}" data-customerType="${row.name}">${self.l('Delete')}</a></li>`,
                            `    </ul>`,
                            `</div>`
                        ].join('');
                    }
                }
            ],
        });

        this._$customerTypeForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$customerTypeForm.valid()) {
                return;
            }

            var customerType = this._$customerTypeForm.serializeFormToObject();

            abp.ui.setBusy(this._$customerTypeModal);

            this._customerTypeService.create(customerType).done(function () {
                self._$customerTypeModal.modal('hide');
                self._$customerTypeForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));
                self._$customerTypeTable.ajax.reload();
            }).always(function () {
                abp.ui.clearBusy(self._$customerTypeModal);
            });
        });

        $(document).on('click', '.delete-customerType', function () {
            var customerTypeid = $(this).attr("data-customerType-id");

            deleteAddress(customerTypeid);
        });

        $(document).on('click', '.edit-customerType', function (e) {
            var customerTypeId = $(this).attr("data-customerType-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'CustomerTypes/EditModal?customerTypeId=' + customerTypeId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#CustomerTypeEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#CustomerTypeCreateModal"]', (e) => {
            $('.nav-tabs a[href="#customerType-details"]').tab('show')
        });

        abp.event.on('customerType.edited', (data) => {
            self._$customerTypeTable.ajax.reload();
        });

        this._$customerTypeModal.on('shown.bs.modal', () => {
            self._$customerTypeModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$customerTypeForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$customerTypeTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$customerTypeTable.ajax.reload();
                return false;
            }
        });
    }

    deleteAddress(customerTypeid) {
        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._customerTypeservice.delete({
                        id: customerTypeid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$customerTypeTable.ajax.reload();
                    });
                }
            }
        );
    }
}

