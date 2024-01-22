(function ($) {
    var _communicationGroupService = abp.services.app.communicationGroup,
        l = abp.localization.getSource('Kaizen'),
        _$modal = $('#CommunicationGroupEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var communicationGroup = _$form.serializeFormToObject();

        communicationGroup.userList = $('#CommunicationGroup_Users').select2('val');

        abp.ui.setBusy(_$form);
        _communicationGroupService.update(communicationGroup).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('communicationGroup.edited', communicationGroup);
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
})(jQuery);