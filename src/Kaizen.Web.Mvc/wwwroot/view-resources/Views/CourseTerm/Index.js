class CourseTerm {

    constructor(courseId) {
        this._courseTermService = abp.services.app.courseTerm;
        this._$courseTermModal = $('#CourseTermCreateModal');
        this._$courseTermForm = this._$courseTermModal.find('form');
        this._$table = $('#CourseTermsTable');
        this.l = abp.localization.getSource('Kaizen');
        this.courseId = courseId;
    }

    setupGrid() {
        let self = this;

        this._$courseTermTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._courseTermService.getAll,
                inputFilter: function () {
                    var filter = $('#CourseTermsSearchForm').serializeFormToObject(true);

                    filter.courseId = self.courseId;

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$courseTermTable.draw(false)
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
                    data: 'lengthOfTerm'
                },
                {
                    targets: 2,
                    data: 'startDate',
                    render: function (data) {
                        return moment(data).format('DD/MM/YYYY');
                    }
                },
                {
                    targets: 3,
                    data: 'endDate',
                    render: function (data) {
                        return moment(data).format('DD/MM/YYYY');
                    }
                },
                {
                    targets: 4,
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
                            `        <li><a class="dropdown-item edit-courseTerm" data-courseTerm-id="${row.id}" data-bs-toggle="modal" data-bs-target="#CourseTermEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-courseTerm" href="#" data-courseTerm-id="${row.id}" data-courseTerm="${row.name}">${self.l('Delete')}</a></li>`,
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

        this._$courseTermForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$courseTermForm.valid()) {
                return;
            }

            var courseTerm = this._$courseTermForm.serializeFormToObject();

            abp.ui.setBusy(this._$courseTermModal);


         
            courseTerm.CourseId = self.courseId;

            this._courseTermService.create(courseTerm).done(function () {
                self._$courseTermModal.modal('hide');
                self._$courseTermForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));
                self._$courseTermTable.ajax.reload();
            }).always(function () {
                abp.ui.clearBusy(self._$courseTermModal);
            });
        });

        $(document).on('click', '.delete-courseTerm', function () {
            var courseTermid = $(this).attr("data-courseTerm-id");

            self.deleteCourseTerm(courseTermid, $(this).attr("data-courseTerm"));
        });

        $(document).on('click', '.edit-courseTerm', function (e) {
            var courseTermId = $(this).attr("data-courseTerm-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'CourseTerms/EditModal?courseTermId=' + courseTermId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#CourseTermEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#CourseTermCreateModal"]', (e) => {
            $('.nav-tabs a[href="#courseTerm-details"]').tab('show')
        });

        abp.event.on('courseTerm.edited', (data) => {
            self._$courseTermTable.ajax.reload();
        });

        this._$courseTermModal.on('shown.bs.modal', () => {
            self._$courseTermModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$courseTermForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$courseTermTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$courseTermTable.ajax.reload();
                return false;
            }
        });
    }

    deleteCourseTerm(courseTermid, userName) {
        let self = this;

        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._courseTermService.delete({
                        id: courseTermid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$courseTermTable.ajax.reload();
                    });
                }
            }
        );
    }
}

