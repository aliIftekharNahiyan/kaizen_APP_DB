
async function SaveFormSubmit(oFormElement) {
    var self = this;
    var _$companyModal = $('#CompanyCreateModal');
    var _$companyModalForm = $('#companyCreateForm');

    if (!_$companyModalForm.valid()) {
        return false;
    }


    abp.ui.setBusy(_$companyModal);
    const formData = new FormData(oFormElement);
    debugger;
    try {
        const response = await fetch(oFormElement.action, {
            method: 'POST',
            body: formData
        });

        if (response.ok) {
            _$companyModal.modal('hide');
            abp.notify.info('SavedSuccessfully');
            abp.event.trigger('company.created', "");
        }
        //alert(response.statusText);

    } catch (error) {
        console.error('Error:', error);
    }
    abp.ui.clearBusy(_$companyModal);
}
