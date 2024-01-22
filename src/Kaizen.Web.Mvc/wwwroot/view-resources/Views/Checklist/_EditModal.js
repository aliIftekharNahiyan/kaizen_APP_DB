(function ($) {
    var _checklistService = abp.services.app.checklist,
        l = abp.localization.getSource('Kaizen'),
        _$modal = $('#ChecklistEditModal'),
        _$form = _$modal.find('form');

    new Checklist().setupDragDrop('#ChecklistEditModal', true, _$modal.find("[name='Id']").val());

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var checklist = _$form.serializeFormToObject();

        var assignedItems = _$form.find("#addedoptions li");

        if (!assignedItems.length) {
            return;
        }

        var checkListItems = [];
        for (var i = 0; i < assignedItems.length; i++) {
            checkListItems.push({
                ChecklistItemId: $(assignedItems[i]).attr("data-id")
            });
        }

        checklist.CheckListItems = checkListItems;

        abp.ui.setBusy(_$form);
        _checklistService.update(checklist).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('checklist.edited', checklist);
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