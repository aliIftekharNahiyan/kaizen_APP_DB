class ContactMethod {

    constructor() {
        this._contactMethodService = abp.services.app.contactMethod;
        this._$contactMethodModal = $('#ContactMethodCreateModal');
        this._$contactMethodForm = this._$contactMethodModal.find('form');
        this._$table = $('#ContactMethodTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        this._$contactMethodTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._contactMethodService.getAll,
                inputFilter: function () {
                    var filter = $('#ContactMethodSearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$contactMethodTable.draw(false)
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
                            `        <li><a class="dropdown-item edit-contactMethod" data-contactMethod-id="${row.id}" data-bs-toggle="modal" data-bs-target="#ContactMethodEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-contactMethod" href="#" data-contactMethod-id="${row.id}" data-contactMethod="${row.name}">${self.l('Delete')}</a></li>`,
                            `    </ul>`,
                            `</div>`
                        ].join('');
                    }
                }
            ],
        });

        this._$contactMethodForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$contactMethodForm.valid()) {
                return;
            }

            var contactMethod = this._$contactMethodForm.serializeFormToObject();

            abp.ui.setBusy(this._$contactMethodModal);

            this._contactMethodService.create(contactMethod).done(function () {
                self._$contactMethodModal.modal('hide');
                self._$contactMethodForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));
                self._$contactMethodTable.ajax.reload();
            }).always(function () {
                abp.ui.clearBusy(self._$contactMethodModal);
            });
        });

        $(document).on('click', '.delete-contactMethod', function () {
            var contactMethodid = $(this).attr("data-contactMethod-id");

            deleteAddress(contactMethodid);
        });

        $(document).on('click', '.edit-contactMethod', function (e) {
            var contactMethodId = $(this).attr("data-contactMethod-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'ContactMethods/EditModal?contactMethodId=' + contactMethodId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#ContactMethodEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#ContactMethodCreateModal"]', (e) => {
            $('.nav-tabs a[href="#contactMethod-details"]').tab('show')
        });

        abp.event.on('contactMethod.edited', (data) => {
            self._$contactMethodTable.ajax.reload();
        });

        this._$contactMethodModal.on('shown.bs.modal', () => {
            self._$contactMethodModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$contactMethodForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$contactMethodTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$contactMethodTable.ajax.reload();
                return false;
            }
        });
    }

    deleteAddress(contactMethodid) {
        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._contactMethodservice.delete({
                        id: contactMethodid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$contactMethodTable.ajax.reload();
                    });
                }
            }
        );
    }
}

