(function ($) {
    var _lookupService = abp.services.app.lookup,
        l = abp.localization.getSource('Kaizen'),
        _$modal = $('#LookupEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var lookup = _$form.serializeFormToObject();
      
        debugger;
        abp.ui.setBusy(_$form);

        abp.ajax({
            url: abp.appPath + 'lookups/edit',
            type: 'POST',
            data: JSON.stringify({ lookup }),
            contentType: 'application/json',
            dataType: 'json',
            success: function (content) {
                _$modal.modal('hide');
                abp.notify.info(l('SavedSuccessfully'));
                abp.event.trigger('lookup.edited', lookup);
                self._$form[0].reset();
            },
            error: function (e) {
                console.log(e);
            }
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });

        return;       
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
})(jQuery);