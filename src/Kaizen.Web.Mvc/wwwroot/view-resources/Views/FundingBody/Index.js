class FundingBody {

    constructor() {
        this._fundingBodyService = abp.services.app.fundingBody;
        this._$fundingBodyModal = $('#FundingBodyCreateModal');
        this._$fundingBodyForm = this._$fundingBodyModal.find('form');
        this._$table = $('#FundingBodyTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        this._$fundingBodyTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._fundingBodyService.getAll,
                inputFilter: function () {
                    var filter = $('#FundingBodySearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$fundingBodyTable.draw(false)
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
                            `        <li><a class="dropdown-item edit-fundingBody" data-fundingBody-id="${row.id}" data-bs-toggle="modal" data-bs-target="#FundingBodyEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-fundingBody" href="#" data-fundingBody-id="${row.id}" data-fundingBody="${row.name}">${self.l('Delete')}</a></li>`,
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

        this._$fundingBodyForm.find('.save-button').off();
        this._$fundingBodyForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$fundingBodyForm.valid()) {
                return;
            }

            var fundingBody = this._$fundingBodyForm.serializeFormToObject();

            abp.ui.setBusy(this._$fundingBodyModal);

            this._fundingBodyService.create(fundingBody).done(function () {
                self._$fundingBodyModal.modal('hide');
                self._$fundingBodyForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));

                if (self._$fundingBodyTable) {
                    self._$fundingBodyTable.ajax.reload();
                }
            }).always(function () {
                abp.ui.clearBusy(self._$fundingBodyModal);
            });
        });

        $(document).on('click', '.delete-fundingBody', function () {
            var fundingBodyid = $(this).attr("data-fundingBody-id");

            self.deleteFundingBody(fundingBodyid, $(this).attr("data-fundingBody"));
        });

        $(document).on('click', '.edit-fundingBody', function (e) {
            var fundingBodyId = $(this).attr("data-fundingBody-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'FundingBodys/EditModal?fundingBodyId=' + fundingBodyId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#FundingBodyEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#FundingBodyCreateModal"]', (e) => {
            $('.nav-tabs a[href="#fundingBody-details"]').tab('show')
        });

        abp.event.on('fundingBody.edited', (data) => {
            self._$fundingBodyTable.ajax.reload();
        });

        this._$fundingBodyModal.on('shown.bs.modal', () => {
            self._$fundingBodyModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$fundingBodyForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$fundingBodyTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$fundingBodyTable.ajax.reload();
                return false;
            }
        });
    }

    deleteFundingBody(fundingBodyid, userName) {
        let self = this;

        console.log(self);

        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._fundingBodyService.delete({
                        id: fundingBodyid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$fundingBodyTable.ajax.reload();
                    });
                }
            }
        );
    }
}

