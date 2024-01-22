CREATE TABLE [dbo].[FundingBody] (
    [Id]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (256) NOT NULL,
    [Deleted] BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    CONSTRAINT [PK_FundingBody] PRIMARY KEY CLUSTERED ([Id] ASC)
);

