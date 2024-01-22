class Helpers {

    constructor() {

    }

    setupSelects(classObjects, parentModal) {
        let self = this;
        let lastModal;
        let isLoop = false;

        // We need to track the last modal open for modal toggling
        $('.modal').on('shown.bs.modal', (ele) => {
            var parentModal = $(ele.relatedTarget).closest(".modal");


            if (parentModal && parentModal.length && !isLoop) {
                isLoop = true;

                let lastModal = '#' + $(parentModal).attr("id");

                $(ele.currentTarget).find(".close-button").attr("data-bs-target", lastModal).attr("data-bs-toggle", "modal").removeAttr("data-bs-dismiss");
                $(ele.currentTarget).find("[data-bs-dismiss]").attr("data-bs-target", lastModal).attr("data-bs-toggle", "modal").removeAttr("data-bs-dismiss");
            }
        })

        let $selectLists = $('.select2');

        if (parentModal) {
            $selectLists = parentModal.find('.select2');
        }

        console.log($selectLists);

        for (let i = 0; i < $selectLists.length; i++) {
            let $selectList = $($selectLists[i]);



            var service = $selectList.attr("data-service");
            var selectedText = $selectList.attr("data-selected-text");
            var selectedValue = $selectList.attr("data-selected-value");
            var inModal = $selectList.closest(".modal");
            var dontShowAdd = $selectList.attr("data-addnew") === "false";


            $selectList.select2({
                ajax: {
                    url: '/api/services/app/' + service + '/GetAll',
                    data: function (params) {
                        var query = {
                            keyword: params.term,
                            skipCount: params.page > 1 ? params.page * 10 : 0
                        }

                        return query;
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;

                        var parsedData = $.map(data.result.items, function (obj) {
                            obj.text = obj.name; // replace name with the property used for the text

                            return obj;
                        });

                        data.result.items = parsedData;

                        return {
                            results: parsedData,
                            pagination: {
                                more: (params.page * 10) < data.result.totalCount
                            }
                        };
                    },
                    delay: 200,
                    dataType: 'json'
                },
                dropdownParent: inModal && inModal.length ? $(inModal) : null
            });


            if (selectedText && selectedText.length) {
                $selectList.append(new Option(selectedText, selectedValue, false, false)).trigger('change');
            }

            if (!dontShowAdd) {
                $selectList.parent().removeClass('col-md-9').addClass('col-md-8').after(`<div class='col-md-1'><a href='#' class='float-end' id='${service}-add-click' data-bs-target="#${service}CreateModal" data-bs-toggle="modal"><i class="far fa-plus-square fa-2x" alt="Can't find it on the list? Add a new one here."></i></a></div>`);
            }
        }

        if (classObjects) {
            for (var o = 0; o < classObjects.length; o++) {
                var serviceInit = new classObjects[o]();
                serviceInit.setupCreate();

            }
        }
    }
}

