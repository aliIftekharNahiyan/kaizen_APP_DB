class SessionGroup {

    constructor(userId) {
        this._sessionGroupService = abp.services.app.sessionGroup;
        this._$sessionGroupModal = $('#SessionGroupCreateModal');
        this._$sessionGroupForm = this._$sessionGroupModal.find('form');
        this._$table = $('#SessionGroupTable');
        this.l = abp.localization.getSource('Kaizen');
        this.userId = userId;
    }

    setupGrid() {
        let self = this;

        this._$sessionGroupTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._sessionGroupService.getAll,
                inputFilter: function () {
                    var filter = $('#SessionGroupSearchForm').serializeFormToObject(true);

                    filter.userId = self.userId;

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$sessionGroupTable.draw(false)
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
                    data: 'supportType.name'
                },
                {
                    targets: 4,
                    data: 'fundingBody.name'
                },
                {
                    targets: 5,
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
                            `        <li><a class="dropdown-item edit-sessionGroup" data-sessionGroup-id="${row.id}" data-bs-toggle="modal" data-bs-target="#SessionGroupEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-sessionGroup" href="#" data-sessionGroup-id="${row.id}" data-sessionGroup="${row.name}">${self.l('Delete')}</a></li>`,
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

        this._$sessionGroupForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$sessionGroupForm.valid()) {
                return;
            }

            var sessionGroup = this._$sessionGroupForm.serializeFormToObject();

            abp.ui.setBusy(this._$sessionGroupModal);


            var datearray = sessionGroup.ExpiryDate.split("/");
            sessionGroup.ExpiryDate = datearray[1] + '/' + datearray[0] + '/' + datearray[2];
            sessionGroup.UserId = self.userId;

            this._sessionGroupService.create(sessionGroup).done(function () {
                self._$sessionGroupModal.modal('hide');
                self._$sessionGroupForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));
                self._$sessionGroupTable.ajax.reload();
            }).always(function () {
                abp.ui.clearBusy(self._$sessionGroupModal);
            });
        });

        $(document).on('click', '.delete-sessionGroup', function () {
            var sessionGroupid = $(this).attr("data-sessionGroup-id");

            self.deleteSessionGroup(sessionGroupid, $(this).attr("data-sessionGroup"));
        });

        $(document).on('click', '.edit-sessionGroup', function (e) {
            var sessionGroupId = $(this).attr("data-sessionGroup-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'SessionGroups/EditModal?sessionGroupId=' + sessionGroupId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#SessionGroupEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#SessionGroupCreateModal"]', (e) => {
            $('.nav-tabs a[href="#sessionGroup-details"]').tab('show')
        });

        abp.event.on('sessionGroup.edited', (data) => {
            self._$sessionGroupTable.ajax.reload();
        });

        this._$sessionGroupModal.on('shown.bs.modal', () => {
            self._$sessionGroupModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$sessionGroupForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$sessionGroupTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$sessionGroupTable.ajax.reload();
                return false;
            }
        });
    }

    deleteSessionGroup(sessionGroupid, userName) {
        let self = this;

        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._sessionGroupService.delete({
                        id: sessionGroupid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$sessionGroupTable.ajax.reload();
                    });
                }
            }
        );
    }
}

