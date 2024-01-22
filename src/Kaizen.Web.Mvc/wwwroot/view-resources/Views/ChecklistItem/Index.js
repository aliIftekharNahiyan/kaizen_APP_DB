class ChecklistItem {

    constructor() {
        this._checklistItemService = abp.services.app.checklistItem;
        this._$checklistItemModal = $('#ChecklistItemCreateModal');
        this._$checklistItemForm = this._$checklistItemModal.find('form');
        this._$table = $('#ChecklistItemTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        this._$checklistItemTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._checklistItemService.getAll,
                inputFilter: function () {
                    var filter = $('#ChecklistItemSearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$checklistItemTable.draw(false)
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
                            `        <li><a class="dropdown-item edit-checklistItem" data-checklistItem-id="${row.id}" data-bs-toggle="modal" data-bs-target="#ChecklistItemEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-checklistItem" href="#" data-checklistItem-id="${row.id}" data-checklistItem="${row.name}">${self.l('Delete')}</a></li>`,
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

        this._$checklistItemForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$checklistItemForm.valid()) {
                return;
            }

            var checkListOptions = todo.getToDoItems();

            if (!checkListOptions.length) {
                todo.showErrors();
                return;

            }
            var checklistItem = this._$checklistItemForm.serializeFormToObject();
            checklistItem.Options = checkListOptions.map(a => {
                return { Name: a.text, Deleted: a.deleted };
            });

            abp.ui.setBusy(this._$checklistItemModal);

            this._checklistItemService.create(checklistItem).done(function () {
                self._$checklistItemModal.modal('hide');
                self._$checklistItemForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));
                self._$checklistItemTable.ajax.reload();
            }).always(function () {
                abp.ui.clearBusy(self._$checklistItemModal);
            });
        });

        $(document).on('click', '.delete-checklistItem', function () {
            var checklistItemid = $(this).attr("data-checklistItem-id");

            self.deleteChecklistItem(checklistItemid, $(this).attr("data-checklistItem"));
        });

        $(document).on('click', '.edit-checklistItem', function (e) {
            var checklistItemId = $(this).attr("data-checklistItem-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'ChecklistItems/EditModal?checklistItemId=' + checklistItemId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#ChecklistItemEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#ChecklistItemCreateModal"]', (e) => {
            $('.nav-tabs a[href="#checklistItem-details"]').tab('show')
        });

        abp.event.on('checklistItem.edited', (data) => {
            self._$checklistItemTable.ajax.reload();
        });

        this._$checklistItemModal.on('shown.bs.modal', () => {
            self._$checklistItemModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$checklistItemForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$checklistItemTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$checklistItemTable.ajax.reload();
                return false;
            }
        });
    }

    deleteChecklistItem(checklistItemid, userName) {
        let self = this;

        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._checklistItemService.delete({
                        id: checklistItemid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$checklistItemTable.ajax.reload();
                    });
                }
            }
        );
    }
}

