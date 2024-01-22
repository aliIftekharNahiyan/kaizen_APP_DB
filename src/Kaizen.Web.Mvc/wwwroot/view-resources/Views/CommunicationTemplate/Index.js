class CommunicationTemplate {

    constructor() {
        this._communicationTemplateService = abp.services.app.communicationTemplate;

        this._$modal = $('#CommunicationTemplateCreateModal');
        this._$form = this._$modal.find('form');
        this._$table = $('#CommunicationTemplateTable');
        this.l = abp.localization.getSource('Kaizen');
    }


    setupGrid() {
        let self = this;

        var _$communicationTemplateTable = self._$table.DataTable({
            paging: true,
            serverSide: true,
            ordering: true,
            listAction: {
                ajaxFunction: self._communicationTemplateService.getAll,
                inputFilter: function () {
                    var filter = $('#CommunicationTemplateSearchForm').serializeFormToObject(true);

                    return filter;
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: () => self._$communicationTemplateTable.draw(false)
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
                            `        <li><a class="dropdown-item" href='/CommunicationTemplates/Edit/${row.id}'>${self.l('Edit')}</a></li>`,
                            `        <li><a class="dropdown-item delete-template" href="#" data-template-id="${row.id}" data-template-name="${row.name}">${self.l('Delete')}</a></li>`,
                            `    </ul>`,
                            `</div>`
                        ].join('');
                    }
                }
            ]
        });

        $(document).on('click', '.delete-template', function () {
            var userId = $(this).attr("data-template-id");
            var userName = $(this).attr('data-template-name');

            deleteTemplate(userId, userName);
        });

        function deleteTemplate(userId, userName) {
            abp.message.confirm(
                abp.utils.formatString(
                    l('AreYouSureWantToDelete'),
                    userName),
                null,
                (isConfirmed) => {
                    if (isConfirmed) {
                        self._communicationTemplateService.delete({
                            id: userId
                        }).done(() => {
                            abp.notify.info(l('SuccessfullyDeleted'));
                            _$communicationTemplateTable.ajax.reload();
                        });
                    }
                }
            );
        }

        $('.btn-search').on('click', (e) => {
            self._$communicationTemplateTable.ajax.reload();
        });

        $('.txt-search').on('keypress', (e) => {
            if (e.which == 13) {
                self._$communicationTemplateTable.ajax.reload();
                return false;
            }
        });
    }

    setupEditor() {
        var quill = new Quill("#snow-editor", { theme: "snow", modules: { toolbar: [[{ font: [] }, { size: [] }], ["bold", "italic", "underline", "strike"], [{ color: [] }, { background: [] }], [{ script: "super" }, { script: "sub" }], [{ header: [!1, 1, 2, 3, 4, 5, 6] }, "blockquote", "code-block"], [{ list: "ordered" }, { list: "bullet" }, { indent: "-1" }, { indent: "+1" }], ["direction", { align: [] }], ["link", "image", "video"], ["clean"]] } });
        var grabHTMLContent = function () {
            $('#CommunicationTemplate_HTMLContent').val($('.ql-editor').html());
        }

        $('#snow-editor').keyup(grabHTMLContent).blur(grabHTMLContent);

        $('.select2').select2();

        $('#CommunicationTemplate_TemplateType').on("change", function () {
            showHideHeaderFooter();
        });

        function showHideHeaderFooter() {
            var val = $('#CommunicationTemplate_TemplateType').val();
            if (val === "1") {
                $('#headerFooter').show();
            } else {
                $('#headerFooter').hide();
            }
        }

        $('.template-picker').on("change", function () {
            showHeaderFooterHtml($(this));
        });

        function showHeaderFooterHtml(element) {
            if (element && element.val() && $('#CommunicationTemplate_TemplateType').val() === "1") {
                fetch("/CommunicationTemplates/DownloadHTML/" + element.val())
                    .then(x => x.text())
                    .then(x => $('#' + element.attr("data-htmlelement")).html(x));
            }
        }

        showHideHeaderFooter();
        showHeaderFooterHtml($('#CommunicationTemplate_HeaderTemplateId'));
        showHeaderFooterHtml($('#CommunicationTemplate_FooterTemplateId'));
;    }
}
