CREATE TABLE [dbo].[ChecklistItemOption] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (256) NOT NULL,
    [ChecklistItemId] BIGINT         NOT NULL,
    [Deleted]         BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    CONSTRAINT [PK_ChecklistItemOption] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ChecklistItemOption_ChecklistItem_ChecklistItemId] FOREIGN KEY ([ChecklistItemId]) REFERENCES [dbo].[ChecklistItem] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ChecklistItemOption_ChecklistItemId]
    ON [dbo].[ChecklistItemOption]([ChecklistItemId] ASC);

