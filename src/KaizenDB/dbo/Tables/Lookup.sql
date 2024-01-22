CREATE TABLE [dbo].[Lookup] (
    [Id]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [LookTypeId]  BIGINT          NOT NULL,
    [Name]        NVARCHAR (250)  NOT NULL,
    [SName]       NVARCHAR (250)  NULL,
    [Description] NVARCHAR (1024) NULL,
    [IsActive]    BIT             NOT NULL,
    [IsDeleted]   BIT             NOT NULL,
    [CreatedBy]   BIGINT          NOT NULL,
    [CreatedDate] DATETIME2 (7)   NOT NULL,
    [UpdatedBy]   BIGINT          NULL,
    [UpdatedDate] DATETIME2 (7)   NULL,
    CONSTRAINT [PK_Lookup] PRIMARY KEY CLUSTERED ([Id] ASC)
);

