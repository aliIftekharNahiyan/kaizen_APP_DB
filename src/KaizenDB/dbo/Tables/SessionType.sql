CREATE TABLE [dbo].[SessionType] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (256) NOT NULL,
    [Description] NVARCHAR (512) NULL,
    [Deleted]     BIT            NOT NULL,
    CONSTRAINT [PK_SessionType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

