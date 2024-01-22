class Disability {

    constructor() {
        this._disabilityService = abp.services.app.disability;
        this._$disabilityModal = $('#DisabilityCreateModal');
        this._$disabilityForm = this._$disabilityModal.find('form');
        this._$table = $('#DisabilityTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        this._$disabilityTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._disabilityService.getAll,
                inputFilter: function () {
                    var filter = $('#DisabilitySearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$disabilityTable.draw(false)
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
                            `        <li><a class="dropdown-item edit-disability" data-disability-id="${row.id}" data-bs-toggle="modal" data-bs-target="#DisabilityEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-disability" href="#" data-disability-id="${row.id}" data-disability="${row.name}">${self.l('Delete')}</a></li>`,
                            `    </ul>`,
                            `</div>`
                        ].join('');
                    }
                }
            ],
        });

        this._$disabilityForm.find('.save-button').off();
        this._$disabilityForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$disabilityForm.valid()) {
                return;
            }

            var disability = this._$disabilityForm.serializeFormToObject();

            abp.ui.setBusy(this._$disabilityModal);

            this._disabilityService.create(disability).done(function () {
                self._$disabilityModal.modal('hide');
                self._$disabilityForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));
                self._$disabilityTable.ajax.reload();
            }).always(function () {
                abp.ui.clearBusy(self._$disabilityModal);
            });
        });

        $(document).on('click', '.delete-disability', function () {
            var disabilityid = $(this).attr("data-disability-id");

            deleteAddress(disabilityid);
        });

        $(document).on('click', '.edit-disability', function (e) {
            var disabilityId = $(this).attr("data-disability-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'Disabilitys/EditModal?disabilityId=' + disabilityId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#DisabilityEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#DisabilityCreateModal"]', (e) => {
            $('.nav-tabs a[href="#disability-details"]').tab('show')
        });

        abp.event.on('disability.edited', (data) => {
            self._$disabilityTable.ajax.reload();
        });

        this._$disabilityModal.on('shown.bs.modal', () => {
            self._$disabilityModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$disabilityForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$disabilityTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$disabilityTable.ajax.reload();
                return false;
            }
        });
    }

    deleteAddress(disabilityid) {
        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._disabilityservice.delete({
                        id: disabilityid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$disabilityTable.ajax.reload();
                    });
                }
            }
        );
    }
}

