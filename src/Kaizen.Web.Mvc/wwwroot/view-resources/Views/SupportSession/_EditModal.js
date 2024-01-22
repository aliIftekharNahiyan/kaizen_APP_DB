(function ($) {
    var _supportSessionService = abp.services.app.supportSession,
        l = abp.localization.getSource('Kaizen'),
        _$modal = $('#SupportSessionEditModal'),
        _$form = _$modal.find('form');

    new Helpers().setupSelects([SupportType, FundingBody], $('#SupportSessionEditModal'));

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var supportSession = _$form.serializeFormToObject();

        abp.ui.setBusy(_$form);
        _supportSessionService.update(supportSession).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('supportSession.edited', supportSession);
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });

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
})(jQuery);