class UserKin {

    constructor() {
        this._userkinService = abp.services.app.userKin;
        this._$userkinModal = $('#UserKinCreateModal');

        this._$userkinForm = this._$userkinModal.find('form');
        this._$table = $('#kinTable');
        this.l = abp.localization.getSource('Kaizen');
    }



    setupGrid() {
        let self = this;
        this._$userKinTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._userkinService.getAll,
                inputFilter: function () {
                    var filter = $('#UsersKinSearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$userKinTable.draw(true)
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
                    data: 'name'
                },
                {
                    targets: 1,
                    data: 'relation.name'
                },
                {
                    targets: 2,
                    data: 'contactType.name'
                    // render: (data, type, row, meta) => {
                    //     console.log(row)
                    //     return [
                    //     console.log('banglaaaaaaa')
                    //     //console.log(row)
                    //       // row
                    //     ].join('');
                    // }
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
                            `        <li><a class="dropdown-item edit-userKin" data-userKin-id="${row.id}" data-bs-toggle="modal" data-bs-target="#UserKinEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-userKin" href="#" data-userKin-id="${row.id}" data-userKin="${row.name}">${self.l('Delete')}</a></li>`,
                            `    </ul>`,
                            `</div>`
                        ].join('');
                    }
                }
            ],
        });
        this._$userkinForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$userkinForm.valid()) {
                return;
            }

            var kin = this._$userkinForm.serializeFormToObject();

            kin.UserId = $("#User_Id").val();


            abp.ui.setBusy(this._$userkinModal);

            this._userkinService.create(kin).done(function () {
                self._$userkinModal.modal('hide');
                self._$userkinForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));
                if (self._$userKinTable) {
                    self._$userKinTable.ajax.reload();

                }
            }).always(function () {
                abp.ui.clearBusy(self._$userkinModal);
            });
        });


        self.setupCreate(self);
    }



    setupCreate(self) {


        $(document).on('click', '.edit-userKin', function (e) {
            var userKinId = $(this).attr("data-userKin-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'UserKins/EditModal?Id=' + userKinId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {

                    $('#UserKinEditModal div.modal-content').html(content);
                },
                error: function (e) {

                }
            });
        });

        abp.event.on('userKin.edited', (data) => {
            self._$userKinTable.ajax.reload();
        });
        $(document).on('click', '.delete-userKin', function () {
            var userKinid = $(this).attr("data-userKin-id");



            console.log(self);

            abp.message.confirm(
                abp.utils.formatString(
                    self.l('AreYouSureWantToDelete'),
                    $(this).attr("data-userKin")),
                null,
                (isConfirmed) => {
                    if (isConfirmed) {
                        self._userkinService.delete({
                            id: userKinid
                        }).done(() => {
                            abp.notify.info(self.l('SuccessfullyDeleted'));
                            self._$userKinTable.ajax.reload();
                        });
                    }
                }
            );
        });
    }
}

