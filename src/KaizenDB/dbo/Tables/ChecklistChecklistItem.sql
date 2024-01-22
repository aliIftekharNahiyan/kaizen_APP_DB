CREATE TABLE [dbo].[ChecklistChecklistItem] (
    [Id]              BIGINT IDENTITY (1, 1) NOT NULL,
    [CheckListId]     BIGINT NOT NULL,
    [ChecklistItemId] BIGINT NOT NULL,
    CONSTRAINT [PK_ChecklistChecklistItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ChecklistChecklistItem_Checklist_CheckListId] FOREIGN KEY ([CheckListId]) REFERENCES [dbo].[Checklist] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ChecklistChecklistItem_ChecklistItem_ChecklistItemId] FOREIGN KEY ([ChecklistItemId]) REFERENCES [dbo].[ChecklistItem] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ChecklistChecklistItem_CheckListId]
    ON [dbo].[ChecklistChecklistItem]([CheckListId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ChecklistChecklistItem_ChecklistItemId]
    ON [dbo].[ChecklistChecklistItem]([ChecklistItemId] ASC);

