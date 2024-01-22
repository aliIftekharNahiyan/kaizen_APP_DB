CREATE TABLE [dbo].[Disability] (
    [Id]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (512) NOT NULL,
    [Deleted] BIT            NOT NULL,
    CONSTRAINT [PK_Disability] PRIMARY KEY CLUSTERED ([Id] ASC)
);

