
async function SaveFormSubmit(oFormElement) {
    var self = this;

    var _$companyModal = $('#CompanyEditModal');
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
            abp.event.trigger('company.edited', "");
        }       

    } catch (error) {
        console.error('Error:', error);
    }
    abp.ui.clearBusy(_$companyModal);
}
