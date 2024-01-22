class SessionType {

    constructor() {
        this._sessionTypeService = abp.services.app.sessionType;
        this._$sessionTypeModal = $('#SessionTypeCreateModal');
        this._$sessionTypeForm = this._$sessionTypeModal.find('form');
        this._$table = $('#SessionTypeTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        this._$sessionTypeTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._sessionTypeService.getAll,
                inputFilter: function () {
                    var filter = $('#SessionTypeSearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$sessionTypeTable.draw(false)
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
                            `        <li><a class="dropdown-item edit-sessionType" data-sessionType-id="${row.id}" data-bs-toggle="modal" data-bs-target="#SessionTypeEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-sessionType" href="#" data-sessionType-id="${row.id}" data-sessionType="${row.name}">${self.l('Delete')}</a></li>`,
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

        this._$sessionTypeForm.find('.save-button').off();
        this._$sessionTypeForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            console.log('fired on save');

            if (!this._$sessionTypeForm.valid()) {
                return;
            }

            var sessionType = this._$sessionTypeForm.serializeFormToObject();

            abp.ui.setBusy(this._$sessionTypeModal);

            this._sessionTypeService.create(sessionType).done(function () {
                self._$sessionTypeModal.modal('hide');
                self._$sessionTypeForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));

                if (self._$sessionTypeTable) {
                    self._$sessionTypeTable.ajax.reload();
                }
            }).always(function () {
                abp.ui.clearBusy(self._$sessionTypeModal);
            });
        });

        $(document).on('click', '.delete-sessionType', function () {
            var sessionTypeid = $(this).attr("data-sessionType-id");

            self.deleteSessionType(sessionTypeid, $(this).attr("data-sessionType"));
        });

        $(document).on('click', '.edit-sessionType', function (e) {
            var sessionTypeId = $(this).attr("data-sessionType-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'SessionTypes/EditModal?sessionTypeId=' + sessionTypeId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#SessionTypeEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#SessionTypeCreateModal"]', (e) => {
            $('.nav-tabs a[href="#sessionType-details"]').tab('show')
        });

        abp.event.on('sessionType.edited', (data) => {
            self._$sessionTypeTable.ajax.reload();
        });

        this._$sessionTypeModal.on('shown.bs.modal', () => {
            self._$sessionTypeModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$sessionTypeForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$sessionTypeTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$sessionTypeTable.ajax.reload();
                return false;
            }
        });
    }

    deleteSessionType(sessionTypeid, userName) {
        let self = this;

        console.log(self);

        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._sessionTypeService.delete({
                        id: sessionTypeid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$sessionTypeTable.ajax.reload();
                    });
                }
            }
        );
    }
}

