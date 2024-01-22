CREATE TABLE [dbo].[Skill] (
    [Id]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)   NULL,
    [Description] NVARCHAR (100)  NULL,
    [SkillLevel]  INT             NOT NULL,
    [Rate]        DECIMAL (18, 2) NULL,
    [IsActive]    BIT             NOT NULL,
    [IsDeleted]   BIT             NOT NULL,
    [CreatedBy]   BIGINT          NOT NULL,
    [CreatedDate] DATETIME2 (7)   NOT NULL,
    [UpdatedBy]   BIGINT          NULL,
    [UpdatedDate] DATETIME2 (7)   NULL,
    CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED ([Id] ASC)
);

