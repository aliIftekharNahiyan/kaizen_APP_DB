CREATE TABLE [dbo].[Note] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedBy]   BIGINT         NOT NULL,
    [Content]     NVARCHAR (MAX) NULL,
    [CreatedDate] DATETIME2 (7)  NOT NULL,
    [NoteType]    INT            NOT NULL,
    [UserId]      BIGINT         NULL,
    [EntityId]    BIGINT         DEFAULT (CONVERT([bigint],(0))) NOT NULL,
    [EntityType]  NVARCHAR (MAX) NULL,
    [Deleted]     BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsImportant] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    CONSTRAINT [PK_Note] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Note_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Note_UserId]
    ON [dbo].[Note]([UserId] ASC);

