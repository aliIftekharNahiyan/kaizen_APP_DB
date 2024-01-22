(function ($) {
    var _historyService = abp.services.app.history,
        l = abp.localization.getSource('Kaizen'),
        _$modal = $('#HistoryEditModal'),
        _$table = _$modal.find("table")

    var _$historyTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        ordering: true,
        listAction: {
            ajaxFunction: _historyService.getForChangeset,
            inputFilter: function () {
                var filter = {};

                filter.changesetId = _$modal.find("#changesetId").val();

                return filter;
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$historyTable.draw(false)
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
                data: 'propertyName'
            },
            {
                targets: 2,
                data: 'originalValue'
            },
            {
                targets: 3,
                data: 'newValue'
            }
        ]
    })
})(jQuery);
