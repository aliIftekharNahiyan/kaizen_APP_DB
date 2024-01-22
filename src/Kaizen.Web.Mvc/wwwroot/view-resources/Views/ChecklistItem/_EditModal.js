(function ($) {
    var _checklistItemService = abp.services.app.checklistItem,
        l = abp.localization.getSource('Kaizen'),
        _$modal = $('#ChecklistItemEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var checklistItem = _$form.serializeFormToObject();

        var checkListOptions = todo.getToDoItems();
        if (!checkListOptions.length) {
            todo.showErrors();
            return;

        }
        checklistItem.Options = checkListOptions.map(a => {
            return { Name: a.text, Id: a.databaseId, Deleted: a.deleted };
        });

        abp.ui.setBusy(_$form);

        _checklistItemService.update(checklistItem).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('checklistItem.edited', checklistItem);
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