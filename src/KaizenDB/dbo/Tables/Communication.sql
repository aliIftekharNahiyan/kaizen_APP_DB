CREATE TABLE [dbo].[Communication] (
    [Id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [CreatedDate] DATETIME2 (7) NOT NULL,
    [Deleted]     BIT           NOT NULL,
    CONSTRAINT [PK_Communication] PRIMARY KEY CLUSTERED ([Id] ASC)
);

