CREATE TABLE [dbo].[SupportType] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (256) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Cost]        FLOAT (53)     NOT NULL,
    [Margin]      INT            NOT NULL,
    [Deleted]     BIT            NOT NULL,
    [CheckListId] BIGINT         NULL,
    CONSTRAINT [PK_SupportType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SupportType_Checklist_CheckListId] FOREIGN KEY ([CheckListId]) REFERENCES [dbo].[Checklist] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SupportType_CheckListId]
    ON [dbo].[SupportType]([CheckListId] ASC);

