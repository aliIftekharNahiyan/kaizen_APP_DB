class PaymentTerm {

    constructor() {
        this._paymentTermService = abp.services.app.paymentTerm;
        this._$paymentTermModal = $('#PaymentTermCreateModal');
        this._$paymentTermForm = this._$paymentTermModal.find('form');
        this._$table = $('#PaymentTermTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        this._$paymentTermTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._paymentTermService.getAll,
                inputFilter: function () {
                    var filter = $('#PaymentTermSearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$paymentTermTable.draw(false)
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
                            `        <li><a class="dropdown-item edit-paymentTerm" data-paymentTerm-id="${row.id}" data-bs-toggle="modal" data-bs-target="#PaymentTermEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-paymentTerm" href="#" data-paymentTerm-id="${row.id}" data-paymentTerm="${row.name}">${self.l('Delete')}</a></li>`,
                            `    </ul>`,
                            `</div>`
                        ].join('');
                    }
                }
            ],
        });

        this._$paymentTermForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$paymentTermForm.valid()) {
                return;
            }

            var paymentTerm = this._$paymentTermForm.serializeFormToObject();

            abp.ui.setBusy(this._$paymentTermModal);

            this._paymentTermService.create(paymentTerm).done(function () {
                self._$paymentTermModal.modal('hide');
                self._$paymentTermForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));
                self._$paymentTermTable.ajax.reload();
            }).always(function () {
                abp.ui.clearBusy(self._$paymentTermModal);
            });
        });

        $(document).on('click', '.delete-paymentTerm', function () {
            var paymentTermid = $(this).attr("data-paymentTerm-id");

            deleteAddress(paymentTermid);
        });

        $(document).on('click', '.edit-paymentTerm', function (e) {
            var paymentTermId = $(this).attr("data-paymentTerm-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'PaymentTerms/EditModal?paymentTermId=' + paymentTermId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#PaymentTermEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#PaymentTermCreateModal"]', (e) => {
            $('.nav-tabs a[href="#paymentTerm-details"]').tab('show')
        });

        abp.event.on('paymentTerm.edited', (data) => {
            self._$paymentTermTable.ajax.reload();
        });

        this._$paymentTermModal.on('shown.bs.modal', () => {
            self._$paymentTermModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$paymentTermForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$paymentTermTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$paymentTermTable.ajax.reload();
                return false;
            }
        });
    }

    deleteAddress(paymentTermid) {
        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._paymentTermservice.delete({
                        id: paymentTermid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$paymentTermTable.ajax.reload();
                    });
                }
            }
        );
    }
}

