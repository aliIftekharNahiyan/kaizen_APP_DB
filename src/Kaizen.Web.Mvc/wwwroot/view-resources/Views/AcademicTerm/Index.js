class AcademicTerm {

    constructor() {
        
        this._academicTermService = abp.services.app.academicTerm;
        this._$academicTermModal = $('#AcademicTermCreateModal');
        this._$academicTermForm = this._$academicTermModal.find('form');
        this._$table = $('#AcademicTermTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        this._$academicTermTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._academicTermService.getAll,
                inputFilter: function () {
                    var filter = $('#AcademicTermSearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$academicTermTable.draw(false)
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
                    data: 'academicYear.name'
                },
                {
                    targets: 6,
                    data: 'archived'
                },
                {
                    targets: 7,
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
                            `        <li><a class="dropdown-item edit-academicTerm" data-academicTerm-id="${row.id}" data-bs-toggle="modal" data-bs-target="#AcademicTermEditModal">${self.l('Edit')}</a></li>`,
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

        this._$academicTermForm.find('.save-button').off();
        this._$academicTermForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            console.log('fired on save');

            if (!this._$academicTermForm.valid()) {
                return;
            }

            var academicTerm = this._$academicTermForm.serializeFormToObject();

            abp.ui.setBusy(this._$academicTermModal);

            this._academicTermService.create(academicTerm).done(function () {
                self._$academicTermModal.modal('hide');
                self._$academicTermForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));

                if (self._$academicTermTable) {
                    self._$academicTermTable.ajax.reload();
                }
            }).always(function () {
                abp.ui.clearBusy(self._$academicTermModal);
            });
        });

        $(document).on('click', '.delete-academicTerm', function () {
            var academicTermid = $(this).attr("data-academicTerm-id");

            self.deleteAcademicTerm(academicTermid, $(this).attr("data-academicTerm"));
        });

        $(document).on('click', '.edit-academicTerm', function (e) {
            var academicTermId = $(this).attr("data-academicTerm-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'AcademicTerms/EditModal?academicTermId=' + academicTermId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#AcademicTermEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#AcademicTermCreateModal"]', (e) => {
            $('.nav-tabs a[href="#academicTerm-details"]').tab('show');
        });

        abp.event.on('academicTerm.edited', (data) => {
            self._$academicTermTable.ajax.reload();
        });

        this._$academicTermModal.on('shown.bs.modal', () => {
            self._$academicTermModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$academicTermForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$academicTermTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$academicTermTable.ajax.reload();
                return false;
            }
        });
    }

    deleteAcademicTerm(academicTermid, userName) {
        let self = this;

        console.log(self);

        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._academicTermService.delete({
                        id: academicTermid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$academicTermTable.ajax.reload();
                    });
                }
            }
        );
    }
}

