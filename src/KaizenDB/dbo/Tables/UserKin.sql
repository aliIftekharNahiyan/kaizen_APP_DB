CREATE TABLE [dbo].[UserKin] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserId]        BIGINT         NOT NULL,
    [Name]          NVARCHAR (256) NULL,
    [RelationId]    BIGINT         NULL,
    [ContactTypeId] BIGINT         NULL,
    [Contact]       NVARCHAR (100) NULL,
    [IsActive]      BIT            NOT NULL,
    [IsDeleted]     BIT            NOT NULL,
    [CreatedBy]     BIGINT         NOT NULL,
    [CreatedDate]   DATETIME2 (7)  NOT NULL,
    [UpdatedBy]     BIGINT         NULL,
    [UpdatedDate]   DATETIME2 (7)  NULL,
    CONSTRAINT [PK_UserKin] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserKin_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserKin_Lookup_ContactTypeId] FOREIGN KEY ([ContactTypeId]) REFERENCES [dbo].[Lookup] ([Id]),
    CONSTRAINT [FK_UserKin_Lookup_RelationId] FOREIGN KEY ([RelationId]) REFERENCES [dbo].[Lookup] ([Id])
);

