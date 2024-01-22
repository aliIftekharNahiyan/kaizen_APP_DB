CREATE TABLE [dbo].[Region] (
    [Id]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (256) NOT NULL,
    [Deleted] BIT            CONSTRAINT [DF_Region_Deleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED ([Id] ASC)
);

