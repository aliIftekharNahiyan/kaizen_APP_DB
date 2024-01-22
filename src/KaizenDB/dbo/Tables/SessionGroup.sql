CREATE TABLE [dbo].[SessionGroup] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (256) NOT NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [SupportTypeId]   BIGINT         NOT NULL,
    [FundingBodyId]   BIGINT         NOT NULL,
    [CreationDate]    DATETIME2 (7)  CONSTRAINT [DF__SessionGr__Creat__2334397B] DEFAULT (getdate()) NOT NULL,
    [ExpiryDate]      DATETIME2 (7)  NOT NULL,
    [AllocatedHours]  INT            NOT NULL,
    [AllocatedBudget] INT            NOT NULL,
    [Deleted]         BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [UserId]          BIGINT         DEFAULT (CONVERT([bigint],(0))) NOT NULL,
    CONSTRAINT [PK_SessionGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SessionGroup_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SessionGroup_FundingBody_FundingBodyId] FOREIGN KEY ([FundingBodyId]) REFERENCES [dbo].[FundingBody] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SessionGroup_SupportType_SupportTypeId] FOREIGN KEY ([SupportTypeId]) REFERENCES [dbo].[SupportType] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_SessionGroup_FundingBodyId]
    ON [dbo].[SessionGroup]([FundingBodyId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SessionGroup_SupportTypeId]
    ON [dbo].[SessionGroup]([SupportTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SessionGroup_UserId]
    ON [dbo].[SessionGroup]([UserId] ASC);

