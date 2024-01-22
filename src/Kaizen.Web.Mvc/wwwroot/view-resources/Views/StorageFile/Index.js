class StorageFile {

                        constructor() {
                            this._storageFileService = abp.services.app.storageFile;
                            this._$storageFileModal = $('#StorageFileCreateModal');
                            this._$storageFileForm = this._$storageFileModal.find('form');
                            this._$table = $('#StorageFileTable');
                            this.l = abp.localization.getSource('Kaizen');
                        }

                        setupGrid() {
                            let self = this;

                            this._$storageFileTable = this._$table.DataTable({
                                paging: true,
                                serverSide: true,
                                ordering: true,
                                listAction: {
                                    ajaxFunction: this._storageFileService.getAll,
                                    inputFilter: function () {
                                        var filter = $('#StorageFileSearchForm').serializeFormToObject(true);

                                        return filter;
                                    }
                                },
                                buttons: [
                                    {
                                        name: 'refresh',
                                        text: '<i class="fas fa-redo-alt"></i>',
                                        action: () => this._$storageFileTable.draw(false)
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
                                                `        <li><a class="dropdown-item edit-storageFile" data-storageFile-id="${row.id}" data-bs-toggle="modal" data-bs-target="#StorageFileEditModal">${self.l('Edit')}</a></li>`,
                                                `        <li><a class="dropdown-item delete-storageFile" href="#" data-storageFile-id="${row.id}" data-storageFile="${row.name}">${self.l('Delete')}</a></li>`,
                                                `    </ul>`,
                                                `</div>`
                                            ].join('');
                                        }
                                    }
                                ],
                            });

                            this._$storageFileForm.find('.save-button').on('click', (e) => {
                                e.preventDefault();

                                if (!this._$storageFileForm.valid()) {
                                    return;
                                }

                                var storageFile = this._$storageFileForm.serializeFormToObject();

                                abp.ui.setBusy(this._$storageFileModal);

                                this._storageFileService.create(storageFile).done(function () {
                                    self._$storageFileModal.modal('hide');
                                    self._$storageFileForm[0].reset();
                                    abp.notify.info(self.l('SavedSuccessfully'));
                                    self._$storageFileTable.ajax.reload();
                                }).always(function () {
                                    abp.ui.clearBusy(self._$storageFileModal);
                                });
                            });

                            $(document).on('click', '.delete-storageFile', function () {
                                var storageFileid = $(this).attr("data-storageFile-id");

                                deleteAddress(storageFileid);
                            });

                            $(document).on('click', '.edit-storageFile', function (e) {
                                var storageFileId = $(this).attr("data-storageFile-id");

                                e.preventDefault();
                                abp.ajax({
                                    url: abp.appPath + 'StorageFiles/EditModal?storageFileId=' + storageFileId,
                                    type: 'POST',
                                    dataType: 'html',
                                    success: function (content) {
                                        $('#StorageFileEditModal div.modal-content').html(content);
                                    },
                                    error: function (e) {
                                    }
                                });
                            });

                            $(document).on('click', 'a[data-bs-target="#StorageFileCreateModal"]', (e) => {
                                $('.nav-tabs a[href="#storageFile-details"]').tab('show')
                            });

                            abp.event.on('storageFile.edited', (data) => {
                                self._$storageFileTable.ajax.reload();
                            });

                            this._$storageFileModal.on('shown.bs.modal', () => {
                                self._$storageFileModal.find('input:not([type=hidden]):first').focus();
                            }).on('hidden.bs.modal', () => {
                                self._$storageFileForm.clearForm();
                            });

                            $('.btn-search').on('click', (e) => {
                                self._$storageFileTable.ajax.reload();
                            });

                            $('.txt-search').on('keypress', (e) => {
                                if (e.which == 13) {
                                    self._$storageFileTable.ajax.reload();
                                   return false;
                                }
                            });
                        }

                        deleteAddress(storageFileid) {
                            abp.message.confirm(
                                abp.utils.formatString(
                                    self.l('AreYouSureWantToDelete'),
                                    userName),
                                null,
                                (isConfirmed) => {
                                    if (isConfirmed) {
                                        self._storageFileservice.delete({
                                            id: storageFileid
                                        }).done(() => {
                                            abp.notify.info(self.l('SuccessfullyDeleted'));
                                            self._$storageFileTable.ajax.reload();
                                        });
                                    }
                                }
                            );
                        }
                    }
   
                