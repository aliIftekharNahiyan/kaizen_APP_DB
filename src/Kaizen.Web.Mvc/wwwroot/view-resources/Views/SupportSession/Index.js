class SupportSession {

    constructor(userId) {
        this._supportSessionService = abp.services.app.supportSession;
        this._$supportSessionModal = $('#SupportSessionCreateModal');
        this._$supportSessionForm = this._$supportSessionModal.find('form');
        this._$table = $('#SupportSessionTable');
        this.l = abp.localization.getSource('Kaizen');
        this.userId = userId;
    }

    setupGrid() {
        let self = this;

        this._$supportSessionTable = this._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: this._supportSessionService.getAll,
                inputFilter: function () {
                    var filter = $('#SupportSessionSearchForm').serializeFormToObject(true);

                    filter.userId = self.userId;

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => this._$supportSessionTable.draw(false)
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
                    data: 'sessionGroup.name'
                },
                {
                    targets: 2,
                    data: 'supportType.name'
                },
                {
                    targets: 3,
                    data: 'fundingBody.name'
                },
                {
                    targets: 4,
                    data: 'sessionStartDate',
                    render: function (data) {
                        return moment(data).format('DD/MM/YYYY HH:mm');
                    }
                },
                {
                    targets: 5,
                    data: 'sessionEndDate',
                    render: function (data) {
                        return moment(data).format('DD/MM/YYYY HH:mm');
                    }
                },
                {
                    targets: 6,
                    data: 'sessionType.name'
                },
                {
                    targets: 7,
                    data: 'statusText'
                },
                {
                    targets: 8,
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
                            `        <li><a class="dropdown-item edit-supportSession" data-supportSession-id="${row.id}" data-bs-toggle="modal" data-bs-target="#SupportSessionEditModal">${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-supportSession" href="#" data-supportSession-id="${row.id}" data-supportSession="${row.name}">${self.l('Delete')}</a></li>`,
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

        mobiscroll.datepicker('.mobidatetimepicker', {
            controls: ['date', 'time'],
            dateFormat: 'DD/MM/YYYY',
            timeFormat: 'HH:mm',
            firstDay: 1,
            responsive: {
                xsmall: {
                    controls: ['date', 'time'],
                    display: 'bottom',
                    touchUi: true
                },
                small: {
                    controls: ['date', 'time'],
                    display: 'anchored',
                    touchUi: true
                },
                custom: { // Custom breakpoint
                    breakpoint: 800,
                    controls: ['date', 'time'],
                    display: 'anchored',
                    touchUi: true
                }
            }
        });


        this._$supportSessionForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$supportSessionForm.valid()) {
                return;
            }

            var supportSession = this._$supportSessionForm.serializeFormToObject();

            abp.ui.setBusy(this._$supportSessionModal);

            supportSession.UserId = self.userId;
            this._supportSessionService.create(supportSession).done(function () {
                self._$supportSessionModal.modal('hide');
                self._$supportSessionForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));
                self._$supportSessionTable.ajax.reload();
            }).always(function () {
                abp.ui.clearBusy(self._$supportSessionModal);
            });
        });

        $(document).on('click', '.delete-supportSession', function () {
            var supportSessionid = $(this).attr("data-supportSession-id");

            self.deleteSupportSession(supportSessionid, $(this).attr("data-supportSession"));
        });

        $(document).on('click', '.edit-supportSession', function (e) {
            var supportSessionId = $(this).attr("data-supportSession-id");

            e.preventDefault();
            abp.ajax({
                url: abp.appPath + 'SupportSessions/EditModal?supportSessionId=' + supportSessionId,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    $('#SupportSessionEditModal div.modal-content').html(content);
                },
                error: function (e) {
                }
            });
        });

        $(document).on('click', 'a[data-bs-target="#SupportSessionCreateModal"]', (e) => {
            $('.nav-tabs a[href="#supportSession-details"]').tab('show')
        });

        abp.event.on('supportSession.edited', (data) => {
            self._$supportSessionTable.ajax.reload();
        });

        this._$supportSessionModal.on('shown.bs.modal', () => {
            self._$supportSessionModal.find('input:not([type=hidden]):first').focus();
        }).on('hidden.bs.modal', () => {
            self._$supportSessionForm.clearForm();
        });

        $('.btn-search').on('click', (e) => {
            self._$supportSessionTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$supportSessionTable.ajax.reload();
                return false;
            }
        });
    }

    setupCalendar() {
        var self = this;

        mobiscroll.datepicker('.mobidatetimepicker', {
            controls: ['date', 'time'],
            dateFormat: 'DD/MM/YYYY',
            timeFormat: 'HH:mm',
            firstDay: 1,
            responsive: {
                xsmall: {
                    controls: ['date', 'time'],
                    display: 'bottom',
                    touchUi: true
                },
                small: {
                    controls: ['date', 'time'],
                    display: 'anchored',
                    touchUi: true
                },
                custom: { // Custom breakpoint
                    breakpoint: 800,
                    controls: ['date', 'time'],
                    display: 'anchored',
                    touchUi: true
                }
            }
        });

        abp.event.on('supportSession.edited', (data) => {
            getCalendarEvents();
        });

        this._$supportSessionForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$supportSessionForm.valid()) {
                return;
            }

            var supportSession = this._$supportSessionForm.serializeFormToObject();

            abp.ui.setBusy(this._$supportSessionModal);

            supportSession.UserId = self.userId;
            this._supportSessionService.create(supportSession).done(function () {
                self._$supportSessionModal.modal('hide');
                self._$supportSessionForm[0].reset();
                abp.notify.info(self.l('SavedSuccessfully'));

                getCalendarEvents();
            }).always(function () {
                abp.ui.clearBusy(self._$supportSessionModal);
            });
        });

        var inst = mobiscroll.eventcalendar('#session-calendar', {
            theme: 'windows',
            themeVariant: 'light',
            clickToCreate: true,
            dragToCreate: false,
            dragToMove: false,
            dragToResize: false,
            eventDelete: false,
            view: {
                calendar: { labels: true },
            },
            onEventClick: function (event, inst) {
                var supportSessionId = event.event.id;

                if (event.event.title != 'New event') {
                    abp.ajax({
                        url: abp.appPath + 'SupportSessions/EditModal?supportSessionId=' + supportSessionId,
                        type: 'POST',
                        dataType: 'html',
                        success: function (content) {
                            $('#SupportSessionEditModal div.modal-content').html(content);

                            $('#SupportSessionEditModal').modal('show');
                        },
                        error: function (e) {
                        }
                    });
                } else {
                    $('input[name="SessionStartDate"]').val(moment(event.event.start.toISOString()).format('L'));
                    $('input[name="SessionEndDate"]').val(moment(event.event.end.toISOString()).format('L'));

                    $('#SupportSessionCreateModal').modal('show');
                }
            },
        });

        getCalendarEvents();

        function getCalendarEvents() {
            self._supportSessionService.getAllForCalendar({ userId: self.userId }).then(function (data) {
                inst.setEvents(data.items);
            });
        }
    }

    deleteSupportSession(supportSessionid, userName) {
        let self = this;

        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._supportSessionService.delete({
                        id: supportSessionid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$supportSessionTable.ajax.reload();
                    });
                }
            }
        );
    }
}

