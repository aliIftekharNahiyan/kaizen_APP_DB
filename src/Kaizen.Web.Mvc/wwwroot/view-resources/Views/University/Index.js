class University {

    constructor() {
        
        this._universityService = abp.services.app.university;
        this._$universityModal = $('#UniversityCreateModal');
        this._$universityForm = this._$universityModal.find('form');
        this._$table = $('#UniversityTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        if (this._$table && this._$table.length) {
            this._$universityTable = this._$table.DataTable({
                paging: true,
                serverSide: true,
                ordering: true,
                listAction: {
                    ajaxFunction: this._universityService.getAll,
                    inputFilter: function () {
                        var filter = $('#UniversitySearchForm').serializeFormToObject(true);

                        return filter;
                    }
                },
                buttons: [
                    {
                        name: 'refresh',
                        text: '<i class="fas fa-redo-alt"></i>',
                        action: () => this._$universityTable.draw(false)
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
                                `        <li><a class="dropdown-item" href='/Universitys/Edit/${row.id}'>${self.l('Edit')}</a></li>`,
                                `        <li><a class="dropdown-item delete-university" href="#" data-university-id="${row.id}" data-university="${row.name}">${self.l('Delete')}</a></li>`,
                                `    </ul>`,
                                `</div>`
                            ].join('');
                        }
                    }
                ],
            });
        }

        self.setupCreate();
    }

    setupCreate() { 
        let self = this;

        self._$universityForm.find('.save-button').off();
        self._$universityForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!self._$universityForm.valid()) {
                return;
            }

            var university = self._$universityForm.serializeFormToObject();

            abp.ui.setBusy(self._$universityModal);

            self._universityService.create(university).done(function () {
                self._$universityModal.modal('hide');
                self._$universityForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));

                if (self._$universityTable && self._$universityTable.length) {
                    self._$universityTable.ajax.reload();
                }
            }).always(function () {
                abp.ui.clearBusy(self._$universityModal);
            });
        });

        $(document).on('click', '.delete-university', function () {
            var universityid = $(this).attr("data-university-id");

            deleteAddress(universityid);
        });

        $(document).on('click', '.edit-university', function (e) {
            var universityId = $(this).attr("data-university-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'Universitys/EditModal?universityId=' + universityId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#UniversityEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#UniversityCreateModal"]', (e) => {
            $('.nav-tabs a[href="#university-details"]').tab('show')
        });

        abp.event.on('university.edited', (data) => {
            self._$universityTable.ajax.reload();
        });

        self._$universityModal.on('shown.bs.modal', () => {
            self._$universityModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$universityForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$universityTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$universityTable.ajax.reload();
                return false;
            }
        });
    }

    deleteAddress(universityid) {
        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._universityservice.delete({
                        id: universityid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$universityTable.ajax.reload();
                    });
                }
            }
        );
    }
}

