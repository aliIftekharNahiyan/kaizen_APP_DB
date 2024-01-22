class Address {

    constructor() {
        this._addressService = abp.services.app.address;
        this._$addressModal = $('#AddressCreateModal');
        this._$addressForm = this._$addressModal.find('form');
        this._$addressTable = $('#AddressTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid(userId) {
        let self = this;

        this._$addressTable = this._$addressTable.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._addressService.getAll,
                inputFilter: function () {
                    var filter = $('#AddressSearchForm').serializeFormToObject(true);

                    filter.userId = userId;

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$addressTable.draw(false)
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
                    data: 'line1'
                },
                {
                    targets: 3,
                    data: 'line2'
                },
                {
                    targets: 4,
                    data: 'line3'
                },
                {
                    targets: 5,
                    data: 'city'
                },
                {
                    targets: 6,
                    data: 'county'
                },
                {
                    targets: 7,
                    data: 'postcode'
                },
                {
                    targets: 8,
                    data: 'isPrimary',
                    render: (data, type, row, meta) => {
                        return row.isPrimary === true ? 'Yes' : 'No';
                    }
                },
                {
                    targets: 9,
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
                            `    <ul class="dropdown-menu dropdown-menu-end" data-popper-placement="fixed">`,
                            `        <li><a class="dropdown-item edit-address" data-address-id="${row.id}" data-bs-toggle="modal" data-bs-target="#AddressEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-address" href="#" data-address-id="${row.id}" data-address-name="${row.name}">${self.l('Delete')}</a></li>`,
                            `    </ul>`,
                            `</div>`
                        ].join('');
                    }
                }
            ],
        });

        this._$addressForm.validate({
            rules: {
                Name: "required",
                Line1: "required",
                City: "required",
                County: "required",
                Postcode: "required"
            }
        });

        this._$addressForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$addressForm.valid()) {
                return;
            }

            var address = this._$addressForm.serializeFormToObject();

            address.userId = userId;

            abp.ui.setBusy(this._$addressModal);

            this._addressService.create(address).done(function () {
                self._$addressModal.modal('hide');
                self._$addressForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));
                self._$addressTable.ajax.reload();
            }).always(function () {
                abp.ui.clearBusy(self._$addressModal);
            });
        });

        $(document).on('click', '.delete-address', function () {
            var addressid = $(this).attr("data-address-id");

            deleteAddress(addressid);
        });

        $(document).on('click', '.edit-address', function (e) {
            var addressId = $(this).attr("data-address-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'Address/EditModal?addressId=' + addressId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#AddressEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#AddressCreateModal"]', (e) => {
            $('.nav-tabs a[href="#address-details"]').tab('show')
        });

        abp.event.on('address.edited', (data) => {
            self._$addressTable.ajax.reload();
        });

        this._$addressModal.on('shown.bs.modal', () => {
            self._$addressModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$addressForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$addressTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$addressTable.ajax.reload();
               return false;
            }
        });
    }

    deleteAddress(addressid) {
        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._addressService.delete({
                        id: addressid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$addressTable.ajax.reload();
                    });
                }
            }
        );
    }
}
   