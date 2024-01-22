class Region {

    constructor() {
        this._regionService = abp.services.app.region;
        this._$regionModal = $('#RegionCreateModal');
        this._$regionForm = this._$regionModal.find('form');
        this._$table = $('#RegionTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        this._$regionTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._regionService.getAll,
                inputFilter: function () {
                    var filter = $('#RegionSearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$regionTable.draw(false)
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
                            `        <li><a class="dropdown-item edit-region" data-region-id="${row.id}" data-bs-toggle="modal" data-bs-target="#RegionEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-region" href="#" data-region-id="${row.id}" data-region="${row.name}">${self.l('Delete')}</a></li>`,
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
        let self = this;

        this._$regionForm.find('.save-button').off();
        this._$regionForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$regionForm.valid()) {
                return;
            }

            var region = this._$regionForm.serializeFormToObject();

            abp.ui.setBusy(this._$regionModal);

            this._regionService.create(region).done(function () {
                self._$regionModal.modal('hide');
                self._$regionForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));
                self._$regionTable.ajax.reload();
            }).always(function () {
                abp.ui.clearBusy(self._$regionModal);
            });
        });

        $(document).on('click', '.delete-region', function () {
            var regionid = $(this).attr("data-region-id");

            deleteAddress(regionid);
        });

        $(document).on('click', '.edit-region', function (e) {
            var regionId = $(this).attr("data-region-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'Regions/EditModal?regionId=' + regionId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#RegionEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#RegionCreateModal"]', (e) => {
            $('.nav-tabs a[href="#region-details"]').tab('show')
        });

        abp.event.on('region.edited', (data) => {
            self._$regionTable.ajax.reload();
        });

        this._$regionModal.on('shown.bs.modal', () => {
            self._$regionModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$regionForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$regionTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$regionTable.ajax.reload();
                return false;
            }
        });
    }

    deleteAddress(regionid) {
        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._regionservice.delete({
                        id: regionid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$regionTable.ajax.reload();
                    });
                }
            }
        );
    }
}

