class Notes {

    constructor() {
        this._noteService = abp.services.app.note;
        this._$noteModal = $('#NoteCreateModal');
        this._$noteForm = this._$noteModal.find('form');
        this.l = abp.localization.getSource('Kaizen');
    }

    getNotes(entityId, entityType) {
        var self = this;

        abp.ui.setBusy(this._$noteModal);

        $('#noteContent .simplebar-content').html('');

        this._noteService.getGrouped({ entityId, entityType }).then((data) => {
            var notesList = ``;

            console.log(data);

            notesList += `<ul class="list-unstyled mb-0">`;

            for (var i = 0; i < data.items.length; i++) {
                var noteGroup = data.items[i];

                notesList += `<li class="chat-day-title">
                                    <div class="title">${noteGroup[0].title}</div>
                                </li>`;

                for (var o = 0; o < noteGroup.length; o++) {
                    var note = noteGroup[o];

                    notesList += `<li>
                                    <div class="conversation-list">
                                     <div class="ctext-wrap">
                                        <div class="ctext-wrap-content">
                                            <h5 class="conversation-name"><a href="#" class="user-name">${note.createdBy}</a> <span class="time">${note.time}</span></h5>
                                            <p class="mb-0">${note.content}</p>
                                        </div>
                                    </div>
                                   </div>
                                   </li>`
                }
            }

            notesList += `</ul>`;

            $('#noteContent .simplebar-content').html(notesList);

            abp.ui.clearBusy(self._$noteModal);
        });
    }

    setupNotes(entityId, entityType) {
        var self = this;


        self.getNotes(entityId, entityType);


        this._$noteForm.find('.save-button').off();
        this._$noteForm.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!this._$noteForm.valid()) {
                return;
            }

            var notes = this._$noteForm.serializeFormToObject();
            notes.entityId = entityId;
            notes.entityType = entityType;

 
            abp.ui.setBusy(this._$noteModal);

            this._noteService.create(notes).done(function () {
                self._$noteForm[0].reset();

                self.getNotes(entityId, entityType);
            }).always(function () {
                abp.ui.clearBusy(self._$noteModal);
            });
        });
    }

    deleteNote(noteName) {
        abp.message.confirm(
            abp.utils.formatString(
                self.l('AreYouSureWantToDelete'),
                noteName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    self._noteservice.delete({
                        id: noteid
                    }).done(() => {
                        abp.notify.info(self.l('SuccessfullyDeleted'));
                        self._$noteTable.ajax.reload();
                    });
                }
            }
        );
    }
}

