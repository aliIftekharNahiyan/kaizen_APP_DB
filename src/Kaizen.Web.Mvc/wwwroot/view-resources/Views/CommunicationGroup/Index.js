class CommunicationGroup {

    constructor() {
        this._communicationGroupService = abp.services.app.communicationGroup;
        this._$communicationGroupModal = $('#CommunicationGroupCreateModal');
        this._$communicationGroupForm = this._$communicationGroupModal.find('form');
        this._$table = $('#CommunicationGroupTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        this._$communicationGroupTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._communicationGroupService.getAll,
                inputFilter: function () {
                    var filter = $('#CompanySearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$communicationGroupTable.draw(false)
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
                            `        <li><a class="dropdown-item edit-communicationGroup" data-communicationGroup-id="${row.id}" data-bs-toggle="modal" data-bs-target="#CommunicationGroupEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-communicationGroup" href="#" data-communicationGroup-id="${row.id}" data-communicationGroup="${row.name}">${self.l('Delete')}</a></li>`,
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

        self._$communicationGroupForm.find('.save-button').off();
        self._$communicationGroupForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!self._$communicationGroupForm.valid()) {
                return;
            }

            var communicationGroup = self._$communicationGroupForm.serializeFormToObject();

            communicationGroup.userList = $('#CommunicationGroup_Users').select2('val');

            abp.ui.setBusy(self._$modal);
            self._communicationGroupService.create(communicationGroup).done(function () {
                self._$modal.modal('hide');
                self._$form[0].reset();
                abp.notify.info(l('SavedSuccessfully'));
                self._$table.ajax.reload();
            }).always(function () {
                abp.ui.clearBusy(self._$communicationGroupModal);
            });
        });

        $(document).on('click', '.delete-communicationGroup', function () {
            var communicationGroupid = $(this).attr("data-communicationGroup-id");            
            self.deleteCommunicationGroup(communicationGroupid, $(this).attr("data-communicationGroup"));
        });

        $(document).on('click', '.edit-communicationGroup', function (e) {
            var communicationGroupId = $(this).attr("data-communicationGroup-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'Communications/GroupsEditModal?communicationGroupId=' + communicationGroupId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#CommunicationGroupEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        abp.event.on('communicationGroup.edited', (data) => {
            self._$communicationGroupTable.ajax.reload();
        });

        abp.event.on('communicationGroup.created', (data) => {
            self._$communicationGroupTable.ajax.reload();
        });

        this._$communicationGroupModal.on('shown.bs.modal', () => {
            self._$communicationGroupModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$communicationGroupForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$communicationGroupTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$communicationGroupTable.ajax.reload();
                return false;
            }
        });
    }

    deleteCommunicationGroup(communicationGroupid, userName) {
        let self = this;

        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._communicationGroupService.delete({
                        id: communicationGroupid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$communicationGroupTable.ajax.reload();
                    });
                }
            }
        );
    }
        
}



