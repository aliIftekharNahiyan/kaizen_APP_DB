class RegionLocation {

    constructor() {
        this._regionLocationService = abp.services.app.regionLocation;
        this._$regionLocationModal = $('#RegionLocationCreateModal');
        this._$regionLocationForm = this._$regionLocationModal.find('form');
        this._$table = $('#RegionLocationTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        this._$regionLocationTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._regionLocationService.getAll,
                inputFilter: function () {
                    var filter = $('#RegionLocationSearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$regionLocationTable.draw(false)
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
                            `        <li><a class="dropdown-item edit-regionLocation" data-regionLocation-id="${row.id}" data-bs-toggle="modal" data-bs-target="#RegionLocationEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-regionLocation" href="#" data-regionLocation-id="${row.id}" data-regionLocation="${row.name}">${self.l('Delete')}</a></li>`,
                            `    </ul>`,
                            `</div>`
                        ].join('');
                    }
                }
            ],
        });

        this._$regionLocationForm.find('.save-button').off();
        this._$regionLocationForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$regionLocationForm.valid()) {
                return;
            }

            var regionLocation = this._$regionLocationForm.serializeFormToObject();

            abp.ui.setBusy(this._$regionLocationModal);

            this._regionLocationService.create(regionLocation).done(function () {
                self._$regionLocationModal.modal('hide');
                self._$regionLocationForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));
                self._$regionLocationTable.ajax.reload();
            }).always(function () {
                abp.ui.clearBusy(self._$regionLocationModal);
            });
        });

        $(document).on('click', '.delete-regionLocation', function () {
            var regionLocationid = $(this).attr("data-regionLocation-id");

            deleteAddress(regionLocationid);
        });

        $(document).on('click', '.edit-regionLocation', function (e) {
            var regionLocationId = $(this).attr("data-regionLocation-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'RegionLocations/EditModal?regionLocationId=' + regionLocationId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#RegionLocationEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#RegionLocationCreateModal"]', (e) => {
            $('.nav-tabs a[href="#regionLocation-details"]').tab('show')
        });

        abp.event.on('regionLocation.edited', (data) => {
            self._$regionLocationTable.ajax.reload();
        });

        this._$regionLocationModal.on('shown.bs.modal', () => {
            self._$regionLocationModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$regionLocationForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$regionLocationTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$regionLocationTable.ajax.reload();
                return false;
            }
        });
    }

    deleteAddress(regionLocationid) {
        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._regionLocationservice.delete({
                        id: regionLocationid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$regionLocationTable.ajax.reload();
                    });
                }
            }
        );
    }
}

