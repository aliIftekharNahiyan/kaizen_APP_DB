CREATE TABLE [dbo].[ContactMethod] (
    [Id]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (128) NOT NULL,
    [Deleted] BIT            NOT NULL,
    CONSTRAINT [PK_ContactMethod] PRIMARY KEY CLUSTERED ([Id] ASC)
);

