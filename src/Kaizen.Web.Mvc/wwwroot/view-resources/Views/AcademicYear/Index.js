class AcademicYear {

    constructor() {
        
        this._academicYearService = abp.services.app.academicYear;
        this._$academicYearModal = $('#AcademicYearCreateModal');
        this._$academicYearForm = this._$academicYearModal.find('form');
        this._$table = $('#AcademicYearTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        this._$academicYearTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._academicYearService.getAll,
                inputFilter: function () {
                    var filter = $('#AcademicYearSearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$academicYearTable.draw(false)
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
                    data: 'startDate',
                    render: function (data) {
                        return moment(data).format('DD/MM/YYYY');
                    }
                },
                {
                    targets: 4,
                    data: 'endDate',
                    render: function (data) {
                        return moment(data).format('DD/MM/YYYY');
                    }
                },
                {
                    targets: 5,
                    data: 'archived'
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
                            `        <li><a class="dropdown-item edit-academicYear" data-academicYear-id="${row.id}" data-bs-toggle="modal" data-bs-target="#AcademicYearEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-academicYear" href="#" data-academicYear-id="${row.id}" data-academicYear="${row.name}">${self.l('Delete')}</a></li>`,
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

        this._$academicYearForm.find('.save-button').off();
        this._$academicYearForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            console.log('fired on save');

            if (!this._$academicYearForm.valid()) {
                return;
            }

            var academicYear = this._$academicYearForm.serializeFormToObject();

            abp.ui.setBusy(this._$academicYearModal);

            this._academicYearService.create(academicYear).done(function () {
                self._$academicYearModal.modal('hide');
                self._$academicYearForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));

                if (self._$academicYearTable) {
                    self._$academicYearTable.ajax.reload();
                }
            }).always(function () {
                abp.ui.clearBusy(self._$academicYearModal);
            });
        });

        $(document).on('click', '.delete-academicYear', function () {
            var academicYearid = $(this).attr("data-academicYear-id");

            self.deleteAcademicYear(academicYearid, $(this).attr("data-academicYear"));
        });

        $(document).on('click', '.edit-academicYear', function (e) {
            var academicYearId = $(this).attr("data-academicYear-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'AcademicYears/EditModal?academicYearId=' + academicYearId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#AcademicYearEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#AcademicYearCreateModal"]', (e) => {
            $('.nav-tabs a[href="#academicYear-details"]').tab('show');
        });

        abp.event.on('academicYear.edited', (data) => {
            self._$academicYearTable.ajax.reload();
        });

        this._$academicYearModal.on('shown.bs.modal', () => {
            self._$academicYearModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$academicYearForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$academicYearTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$academicYearTable.ajax.reload();
                return false;
            }
        });
    }

    deleteAcademicYear(academicYearid, userName) {
        let self = this;

        console.log(self);

        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._academicYearService.delete({
                        id: academicYearid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$academicYearTable.ajax.reload();
                    });
                }
            }
        );
    }
}

