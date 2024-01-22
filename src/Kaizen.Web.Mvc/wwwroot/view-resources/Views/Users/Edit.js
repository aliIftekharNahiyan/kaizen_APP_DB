class User {

    constructor() {
        this._userService = abp.services.app.user,
            this.l = abp.localization.getSource('Kaizen'),
            this._$form = $('#edit-user-details-form'),

            this.firstLoads = {
                'Addresses': false,
                'History': false,
                'SessionGroups': false,
                'SupportSessions': false,
                'SessionCalendar': false
            }

    }
    setupGrid() {
        let self = this;
        const userId = $('#User_Id').val();
        const entityType = $('#EntityType').val();

        const resultStatus = $("#resultStatus").val();

        if (resultStatus == 1) {
            abp.notify.info(self.l('SavedSuccessfully'));
        }

        // This is needed for multiple tabs.
        var supportSession = new SupportSession(userId);

        $('a[data-bs-toggle="tab"]').on("click", function () {
           

            switch ($(this).attr("href")) {
                case "#edit-user-addresses":
                    if (self.firstLoads.Addresses === false) {
                        var address = new Address();

                        address.setupGrid(userId);
                        self.firstLoads.Addresses = true;
                    }

                    break;
                case "#edit-user-history":
                    if (self.firstLoads.History === false) {
                        var history = new Histories(entityType, userId);

                        history.setupGrid();
                        self.firstLoads.History = true;
                    }

                    break;
                case "#edit-user-session-groups":
                    if (self.firstLoads.SessionGroups === false) {
                        var sessionGroup = new SessionGroup(userId);

                        sessionGroup.setupGrid();
                        self.firstLoads.SessionGroups = true;
                    }

                    break;
                case "#edit-user-sessions":
                    if (self.firstLoads.SupportSessions === false) {
                        supportSession.setupGrid();
                        self.firstLoads.SupportSessions = true;
                    }

                    break;
                case "#edit-user-calendar":
                    if (self.firstLoads.SessionCalendar === false) {
                        supportSession.setupCalendar();
                        self.firstLoads.SessionCalendar = true;
                    }
                    break;
            }
        });
        this._$form.closest('div').find(".save-button-details").click(function (e) {
            e.preventDefault();

            save(e);
        });
        function save(e) {
            if (!self._$form.valid()) {
                return;
            }
            self._$form.submit();


        }
    }
}