class Skill {

    constructor() {

        this._skillService = abp.services.app.skill;
        this._$skillModal = $('#skillCreateModal');
        this._$skillForm = this._$skillModal.find('form');
        this._$table = $('#skillTable');
        this.l = abp.localization.getSource('Kaizen');
    }

    setupGrid() {
        let self = this;

        if (this._$table && this._$table.length) {
            this._$skillTable = this._$table.DataTable({
                paging: true,
                serverSide: true,
                ordering: true,
                listAction: {
                    ajaxFunction: this._skillService.getAll,
                    inputFilter: function () {
                        var filter = $('#skillSearchForm').serializeFormToObject(true);

                        return filter;
                    }
                },
                buttons: [
                    {
                        name: 'refresh',
                        text: '<i class="fas fa-redo-alt"></i>',
                        action: () => this._$skillTable.draw(false)
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
                                `        <li><a class="dropdown-item" href='/skills/Edit/${row.id}'>${self.l('Edit')}</a></li>`,
                                `        <li><a class="dropdown-item delete-skill" href="#" data-skill-id="${row.id}" data-skill="${row.name}">${self.l('Delete')}</a></li>`,
                                `    </ul>`,
                                `</div>`
                            ].join('');
                        }
                    }
                ],
            });
        }

        self.setupCreate();
    }

    setupCreate() {
        let self = this;

        self._$skillForm.find('.save-button').off();
        self._$skillForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!self._$skillForm.valid()) {
                return;
            }

            var skill = self._$skillForm.serializeFormToObject();

            abp.ui.setBusy(self._$skillModal);

            self._skillService.create(skill).done(function () {
                self._$skillModal.modal('hide');
                self._$skillForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));

                if (self._$skillTable && self._$skillTable.length) {
                    self._$skillTable.ajax.reload();
                }
            }).always(function () {
                abp.ui.clearBusy(self._$skillModal);
            });
        });

        $(document).on('click', '.delete-skill', function () {
            var skillid = $(this).attr("data-skill-id");

            deleteAddress(skillid);
        });

        $(document).on('click', '.edit-skill', function (e) {
            var skillId = $(this).attr("data-skill-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'skills/EditModal?skillId=' + skillId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#skillEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#skillCreateModal"]', (e) => {
            $('.nav-tabs a[href="#skill-details"]').tab('show')
        });

        abp.event.on('skill.edited', (data) => {
            self._$skillTable.ajax.reload();
        });

        self._$skillModal.on('shown.bs.modal', () => {
            self._$skillModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$skillForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$skillTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$skillTable.ajax.reload();
                return false;
            }
        });
    }

    deleteAddress(skillid) {
        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._skillservice.delete({
                        id: skillid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$skillTable.ajax.reload();
                    });
                }
            }
        );
    }
}

