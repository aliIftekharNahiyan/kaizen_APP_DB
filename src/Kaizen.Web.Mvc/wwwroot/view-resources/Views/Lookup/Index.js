class Lookup {

    constructor() {
        this._lookupService = abp.services.app.lookup;
        this._$lookupModal = $('#LookupCreateModal');
        this._$lookupForm = this._$lookupModal.find('form');
        this._$table = $('#LookupTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        this._$lookupTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._lookupService.getAll,
                inputFilter: function () {
                    var filter = $('#LookupSearchForm').serializeFormToObject(true);
                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$lookupTable.draw(false)
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
                    data: 'lookupType'                    
                },
                {
                    targets: 2,
                    data: 'name'
                },
                {
                    targets: 3,
                    data: 'sName'
                },
                {
                    targets: 4,
                    data: 'description'
                },               
                {
                    targets: 5,
                    data: 'isActive',
                    render: (data, type, row, meta) => {
                        if (data === true) return "Active";
                        else return "Inactive";
                    }
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
                            `        <li><a class="dropdown-item edit-lookup" data-lookup-id="${row.id}" data-bs-toggle="modal" data-bs-target="#LookupEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-lookup" href="#" data-lookup-id="${row.id}" data-lookup="${row.name}">${self.l('Delete')}</a></li>`,
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

        this._$lookupForm.find('.save-button').off();
        this._$lookupForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$lookupForm.valid()) {
                return;
            }

            var lookup = this._$lookupForm.serializeFormToObject();
            lookup.IsActive = $('#IsActive').is(':checked');
            debugger;
            
            abp.ui.setBusy(this._$lookupModal);

            abp.ajax({
                url: abp.appPath + 'Lookups/Create',
                type: 'POST',
                data: JSON.stringify(lookup),
                contentType: 'application/json',
                dataType: 'json',                
                success: function (content) {
                    self._$lookupModal.modal('hide');
                    self._$lookupForm[0].reset();
                    abp.notify.info(self.l('SavedSuccessfully'));
                    if (self._$lookupTable) {
                        self._$lookupTable.ajax.reload();
                    }
                },
                error: function (e) {
                    console.log(e);
                }               
            }).always(function () {
                abp.ui.clearBusy(self._$lookupModal);
            });

            //this._lookupService.create(lookup).done(function () {
            //    self._$lookupModal.modal('hide');
            //    self._$lookupForm[0].reset();
            //    abp.notify.info(self.l('SavedSuccessfully'));

            //    if (self._$lookupTable) {
            //        self._$lookupTable.ajax.reload();
            //    }
            //}).always(function () {
            //    abp.ui.clearBusy(self._$lookupModal);
            //});
        });

        $(document).on('click', '.delete-lookup', function () {
            var lookupid = $(this).attr("data-lookup-id");

            self.deleteLookup(lookupid, $(this).attr("data-lookup"));
        });

        $(document).on('click', '.edit-lookup', function (e) {
            var lookupId = $(this).attr("data-lookup-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'Lookups/EditModal?lookupId=' + lookupId,
                type: 'GET',
                dataType: 'html',
                success: function (content) {
                    $('#LookupEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#LookupCreateModal"]', (e) => {
            $('.nav-tabs a[href="#lookup-details"]').tab('show')
        });

        abp.event.on('lookup.edited', (data) => {
            self._$lookupTable.ajax.reload();
        });

        this._$lookupModal.on('shown.bs.modal', () => {
            self._$lookupModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$lookupForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$lookupTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$lookupTable.ajax.reload();
                return false;
            }
        });
    }

    deleteLookup(lookupid, userName) {
        let self = this;

        console.log(self);

        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {

                    abp.ajax({
                        url: abp.appPath + 'Lookups/Delete?id=' + lookupid,
                        type: 'POST',
                        dataType: 'html',
                        success: function (content) {
                            self._$lookupModal.modal('hide');
                            self._$lookupForm[0].reset();
                            abp.notify.info(self.l('SavedSuccessfully'));
                            if (self._$lookupTable) {
                                self._$lookupTable.ajax.reload();
                            }
                        },
                        error: function (e) {
                            console.log(e);
                        }
                    }).always(function () {
                        abp.ui.clearBusy(self._$lookupModal);
                    });

                    //self._lookupService.delete({
                    //    id: lookupid
                    //}).done(() => {
                    //    abp.notify.info(self.l('SuccessfullyDeleted'));
                    //    self._$lookupTable.ajax.reload();
                    //});
                }
            }
        );
    }
}

