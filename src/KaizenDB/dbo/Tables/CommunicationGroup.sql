CREATE TABLE [dbo].[CommunicationGroup] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (256) NOT NULL,
    [Description] NVARCHAR (512) NOT NULL,
    [Deleted]     BIT            NOT NULL,
    CONSTRAINT [PK_CommunicationGroup] PRIMARY KEY CLUSTERED ([Id] ASC)
);

