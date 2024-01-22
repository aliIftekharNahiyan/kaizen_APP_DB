CREATE TABLE [dbo].[Company] (
    [Id]                   BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (256) NOT NULL,
    [Description]          NVARCHAR (512) NOT NULL,
    [SendFromEmailAddress] NVARCHAR (256) NOT NULL,
    [IsActive]             BIT            NOT NULL,
    [Deleted]              BIT            NOT NULL,
    [LogoFileId]           BIGINT         NOT NULL,
    [PrimaryBrandColour]   NVARCHAR (8)   NULL,
    [SecondaryBrandColour] NVARCHAR (8)   NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED ([Id] ASC)
);

