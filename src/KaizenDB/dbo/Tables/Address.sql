CREATE TABLE [dbo].[Address] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (256) NOT NULL,
    [Line1]     NVARCHAR (256) NOT NULL,
    [Line2]     NVARCHAR (256) NULL,
    [Line3]     NVARCHAR (256) NULL,
    [City]      NVARCHAR (256) NOT NULL,
    [County]    NVARCHAR (256) NOT NULL,
    [Postcode]  NVARCHAR (32)  NOT NULL,
    [IsPrimary] BIT            NOT NULL,
    [Deleted]   BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([Id] ASC)
);

