class Checklist {
    constructor() {
        this._checklistService = abp.services.app.checklist;
        this._$checklistModal = $('#ChecklistCreateModal');
        this._$checklistForm = this._$checklistModal.find('form');
        this._$table = $('#ChecklistTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        this._$checklistTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._checklistService.getAll,
                inputFilter: function () {
                    var filter = $('#ChecklistSearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$checklistTable.draw(false)
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
                            `        <li><a class="dropdown-item edit-checklist" data-checklist-id="${row.id}" data-bs-toggle="modal" data-bs-target="#ChecklistEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-checklist" href="#" data-checklist-id="${row.id}" data-checklist="${row.name}">${self.l('Delete')}</a></li>`,
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

        this._$checklistForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$checklistForm.valid()) {
                return;
            }

            var checklist = self._$checklistForm.serializeFormToObject();

            var assignedItems = self._$checklistModal.find("#addedoptions li");

            if (!assignedItems.length) {
                return;
            }

            abp.ui.setBusy(this._$checklistModal);

            var checkListItems = [];
            for (var i = 0; i < assignedItems.length; i++) {
                checkListItems.push({
                    ChecklistItemId: $(assignedItems[i]).attr("data-id")
                });
            }

            checklist.CheckListItems = checkListItems;

            this._checklistService.create(checklist).done(function () {
                self._$checklistModal.modal('hide');
                self._$checklistForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));
                self._$checklistTable.ajax.reload();
            }).always(function () {
                abp.ui.clearBusy(self._$checklistModal);
            });
        });

        $(document).on('click', '.delete-checklist', function () {
            var checklistid = $(this).attr("data-checklist-id");

            deleteChecklist(checklistid, $(this).attr("data-checklist"));
        });

        $(document).on('click', '.edit-checklist', function (e) {
            var checklistId = $(this).attr("data-checklist-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'Checklists/EditModal?checklistId=' + checklistId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#ChecklistEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#ChecklistCreateModal"]', (e) => {
            $('.nav-tabs a[href="#checklist-details"]').tab('show')
        });

        abp.event.on('checklist.edited', (data) => {
            self._$checklistTable.ajax.reload();
        });

        this._$checklistModal.on('shown.bs.modal', () => {
            self._$checklistModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$checklistForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$checklistTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$checklistTable.ajax.reload();
                return false;
            }
        });
    }

    deleteChecklist(checklistid, userName) {
        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._checklistService.delete({
                        id: checklistid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$checklistTable.ajax.reload();
                    });
                }
            }
        );
    }

    setupDragDrop(parentElement, editMode, id) {
        var self = this;

        abp.ui.setBusy(this._$checklistModal);

        abp.services.app.checklistItem.getAll({}).then(function (data) {
            if (editMode) {
                abp.services.app.checklist.getItemsForChecklist({ id: id }).then(function (checklistData) { 
                    for (var i = 0; i < checklistData.length; i++) {

                        data.items = data.items.filter(a => {
                            return a.id != checklistData[i].checklistItemId;
                        });

                        $(`${parentElement} #addedoptions`).append(`<li data-id="${checklistData[i].checklistItemId}" class="list-group-item">${checklistData[i].checklistItem.name}</li>`);
                    }

                    for (var i = 0; i < data.items.length; i++) {
                        $(`${parentElement} #addoptions`).append(`<li data-id="${data.items[i].id}" class="list-group-item">${data.items[i].name}</li>`);
                    }

                    Sortable.create($(`${parentElement} #addoptions`)[0], {
                        animation: 100,
                        group: 'list-1',
                        draggable: '.list-group-item',
                        handle: '.list-group-item',
                        sort: false,
                        filter: '.sortable-disabled',
                        chosenClass: 'active'
                    });

                    Sortable.create($(`${parentElement} #addedoptions`)[0], {
                        group: 'list-1',
                        handle: '.list-group-item',
                        chosenClass: 'active'
                    });

                    abp.ui.clearBusy(self._$checklistModal);
                });
            }
        })
        
    }
}

