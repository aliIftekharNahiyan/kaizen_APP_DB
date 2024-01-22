CREATE TABLE [dbo].[Invoice] (
    [Id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [CreatedDate] DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED ([Id] ASC)
);

