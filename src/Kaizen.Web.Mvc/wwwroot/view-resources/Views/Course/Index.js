class Course {

    constructor(universityId) {
        this._courseService = abp.services.app.course;
        this._$courseModal = $('#CourseCreateModal');
        this._$courseForm = this._$courseModal.find('form');
        this._$table = $('#CoursesTable');
        this.l = abp.localization.getSource('Kaizen');
        this.universityId = universityId;
    }

    setupGrid() {
        let self = this;

        this._$courseTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._courseService.getAll,
                inputFilter: function () {
                    var filter = $('#CoursesSearchForm').serializeFormToObject(true);

                    filter.universityId = self.universityId;

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$courseTable.draw(false)
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
                            `        <li><a class="dropdown-item" href='/Courses/Edit/${row.id}'>${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-course" href="#" data-course-id="${row.id}" data-course="${row.name}">${self.l('Delete')}</a></li>`,
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

        this._$courseForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$courseForm.valid()) {
                return;
            }

            var course = this._$courseForm.serializeFormToObject();

            abp.ui.setBusy(this._$courseModal);


         
            course.UniversityId = self.universityId;

            this._courseService.create(course).done(function () {
                self._$courseModal.modal('hide');
                self._$courseForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));
                self._$courseTable.ajax.reload();
            }).always(function () {
                abp.ui.clearBusy(self._$courseModal);
            });
        });

        $(document).on('click', '.delete-course', function () {
            var courseid = $(this).attr("data-course-id");

            self.deleteCourse(courseid, $(this).attr("data-course"));
        });

        $(document).on('click', '.edit-course', function (e) {
            var courseId = $(this).attr("data-course-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'Courses/EditModal?courseId=' + courseId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#CourseEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#CourseCreateModal"]', (e) => {
            $('.nav-tabs a[href="#course-details"]').tab('show')
        });

        abp.event.on('course.edited', (data) => {
            self._$courseTable.ajax.reload();
        });

        this._$courseModal.on('shown.bs.modal', () => {
            self._$courseModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$courseForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$courseTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$courseTable.ajax.reload();
                return false;
            }
        });
    }

    deleteCourse(courseid, userName) {
        let self = this;

        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._courseService.delete({
                        id: courseid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$courseTable.ajax.reload();
                    });
                }
            }
        );
    }
}

