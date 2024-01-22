class Histories {

    constructor(entityType, entityId) {
        this.entityType = entityType;
        this.entityId = entityId;

        this._historyService = abp.services.app.history;
        this._$historyModal = $('#HistoryEditModal');
        this._$historyForm = this._$historyModal.find('form');
        this._$table = $('#HistoryTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        var _$historyTable = self._$table.DataTable({
            serverSide: false,
            ordering: true,
            listAction: {
                ajaxFunction: self._historyService.getChangesetsForEntity,
                inputFilter: function () {
                    var filter = {};

                    filter.entityType = self.entityType;
                    filter.entityId = self.entityId;

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => self._$historyTable.draw(false)
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
                    data: 'personName',
                    sortable: true
                },
                {
                    targets: 2,
                    data: 'changeType',
                    sortable: true
                },
                {
                    targets: 3,
                    data: 'creationTime',
                    sortable: true
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
                            `    <ul class="dropdown-menu dropdown-menu-end" data-popper-placement="fixed">`,
                            `        <li><a class="dropdown-item edit-changeset" data-changeset-id="${row.id}" data-bs-toggle="modal" data-bs-target="#HistoryEditModal">${self.l('ViewChangeset')}</a></li>`,
                            `    </ul>`,
                            `</div>`
                        ].join('');
                    }
                }
            ]
        });

        $(document).on('click', '.edit-changeset', function (e) {
            var changesetId = $(this).attr("data-changeset-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'History/EditModal?changesetId=' + changesetId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#HistoryEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });
    }
}
