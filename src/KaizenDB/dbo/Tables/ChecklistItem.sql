CREATE TABLE [dbo].[ChecklistItem] (
    [Id]      BIGINT          IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (1024) NOT NULL,
    [Deleted] BIT             DEFAULT (CONVERT([bit],(0))) NOT NULL,
    CONSTRAINT [PK_ChecklistItem] PRIMARY KEY CLUSTERED ([Id] ASC)
);

