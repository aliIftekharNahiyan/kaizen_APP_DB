class InfoCheckList {

    constructor() {
        
        this._infoCheckListService = abp.services.app.infoCheckList;
        this._$table = $('#InfoCheckListTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        this._$infoCheckListTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._infoCheckListService.getAll,
                inputFilter: function () {
                    var filter = $('#InfoCheckListSearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$infoCheckListTable.draw(false)
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
                    data: 'firstName'
                },
                {
                    targets: 2,
                    data: 'surname'
                },
                {
                    targets: 3,
                    data: 'email'
                },
                {
                    targets: 4,
                    data: 'dateAnswered',
                    render: function (data) {
                        return moment(data).format('DD/MM/YYYY');
                    }
                },
                {
                    targets: 5,
                    data: 'courseStartDate',
                    render: function (data) {
                        return moment(data).format('DD/MM/YYYY');
                    }
                },
                {
                    targets: 6,
                    data: 'courseStartDate',
                    render: function (data) {
                        return moment(data).format('DD/MM/YYYY');
                    }
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
                            `        <li><a class="dropdown-item" href='/InfoCHeckLists/Edit/${row.id}'>${self.l('Edit')}</a></li>`,
                            `    </ul>`,
                            `</div>`
                        ].join('');
                    }
                }
            ],
        });

        self.setupCreate();
    }



    deleteInfoCheckList(infoCheckListid, userName) {
        let self = this;

        console.log(self);

        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._infoCheckListService.delete({
                        id: infoCheckListid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$infoCheckListTable.ajax.reload();
                    });
                }
            }
        );
    }
}

