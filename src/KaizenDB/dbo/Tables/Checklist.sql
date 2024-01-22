CREATE TABLE [dbo].[Checklist] (
    [Id]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (512) NOT NULL,
    [Deleted] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    CONSTRAINT [PK_Checklist] PRIMARY KEY CLUSTERED ([Id] ASC)
);

