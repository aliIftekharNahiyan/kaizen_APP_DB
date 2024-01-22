class Communication {

    constructor() {
        this._communicationService = abp.services.app.communication;

        this._$modal = $('#CommunicationCreateModal');
        this._$form = this._$modal.find('form');
        this._$table = $('#CommunicationTable');
        this.l = abp.localization.getSource('Kaizen');
    }


    setupGrid() {
        let self = this;

        var _$communicationTable = self._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: self._communicationService.getAll,
                inputFilter: function () {
                    var filter = $('#CommunicationSearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => self._$communicationTable.draw(false)
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
                    data: 'name',
                    sortable: false
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
                            `    <ul class="dropdown-menu dropdown-menu-end" data-popper-placement="fixed">`,
                            `        <li><a class="dropdown-item" href='/CommunicationTemplates/Edit/${row.id}'>${self.l('Edit')}</a></li>`
                            `    </ul>`,
                            `</div>`
                        ].join('');
                    }
                }
            ]
        });

        $('.btn-search').on('click', (e) => {
            self._$communicationTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$communicationTable.ajax.reload();
                return false;
            }
        });
    }

    setupEditor() {
        new Quill("#snow-editor", { theme: "snow", modules: { toolbar: [[{ font: [] }, { size: [] }], ["bold", "italic", "underline", "strike"], [{ color: [] }, { background: [] }], [{ script: "super" }, { script: "sub" }], [{ header: [!1, 1, 2, 3, 4, 5, 6] }, "blockquote", "code-block"], [{ list: "ordered" }, { list: "bullet" }, { indent: "-1" }, { indent: "+1" }], ["direction", { align: [] }], ["link", "image", "video"], ["clean"]] } });
        new Quill("#snow-editor-header", { theme: "snow", modules: { toolbar: [[{ font: [] }, { size: [] }], ["bold", "italic", "underline", "strike"], [{ color: [] }, { background: [] }], [{ script: "super" }, { script: "sub" }], [{ header: [!1, 1, 2, 3, 4, 5, 6] }, "blockquote", "code-block"], [{ list: "ordered" }, { list: "bullet" }, { indent: "-1" }, { indent: "+1" }], ["direction", { align: [] }], ["link", "image", "video"], ["clean"]] } });
        new Quill("#snow-editor-footer", { theme: "snow", modules: { toolbar: [[{ font: [] }, { size: [] }], ["bold", "italic", "underline", "strike"], [{ color: [] }, { background: [] }], [{ script: "super" }, { script: "sub" }], [{ header: [!1, 1, 2, 3, 4, 5, 6] }, "blockquote", "code-block"], [{ list: "ordered" }, { list: "bullet" }, { indent: "-1" }, { indent: "+1" }], ["direction", { align: [] }], ["link", "image", "video"], ["clean"]] } });

        $('.select2').select2();

        $('.template-picker').on("change", function () {
            console.log('template changed');
            showHeaderFooterHtml($(this));
        });


        $('#ContentTemplateId').on("change", function () {
            // Get header/footer for this template
            getMainTemplate();
        });

        function getMainTemplate() {
            fetch("/CommunicationTemplates/GetTemplate/" + $('#ContentTemplateId').val())
                .then(x => x.json())
                .then(x => chooseHeaderFooter(x));
        }

        function chooseHeaderFooter(data) {
            $('#HeaderTemplateId').val(data.result.headerTemplateId).trigger("change");
            $('#FooterTemplateId').val(data.result.footerTemplateId).trigger("change");
        }

        function showHeaderFooterHtml(element) {
            if (element && element.val()) {
                var div = document.getElementById(element.attr("data-htmlelement"));
                console.log(div);
                var quill_instance = Quill.find(div);
                console.log(quill_instance);

                fetch("/CommunicationTemplates/DownloadHTML/" + element.val())
                    .then(x => x.text())
                    .then(x => quill_instance.root.innerHTML = x);
            }
        }

        $(function () {
            showHeaderFooterHtml($('#ContentTemplateId'));
            getMainTemplate();
        });
;    }
}
