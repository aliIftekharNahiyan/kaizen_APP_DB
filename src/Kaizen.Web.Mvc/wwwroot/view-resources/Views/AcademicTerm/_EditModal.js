(function ($) {
    var _academicTermService = abp.services.app.academicTerm,
        l = abp.localization.getSource('Kaizen'),
        _$modal = $('#AcademicTermEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var academicTerm = _$form.serializeFormToObject();

        abp.ui.setBusy(_$form);
        _academicTermService.update(academicTerm).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('academicTerm.edited', academicTerm);
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


    mobiscroll.datepicker('.mobipicker', {
        controls: ['date'],
        dateFormat: 'DD/MM/YYYY',
        firstDay: 1,
        responsive: {
            xsmall: {
                controls: ['date'],
                display: 'bottom',
                touchUi: true
            },
            small: {
                controls: ['date'],
                display: 'anchored',
                touchUi: true
            },
            custom: { // Custom breakpoint
                breakpoint: 800,
                controls: ['date'],
                display: 'anchored',
                touchUi: true
            }
        }
    });
})(jQuery);