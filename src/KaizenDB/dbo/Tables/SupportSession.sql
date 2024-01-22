CREATE TABLE [dbo].[SupportSession] (
    [Id]                        BIGINT        IDENTITY (1, 1) NOT NULL,
    [SessionGroupId]            BIGINT        NOT NULL,
    [SupportTypeId]             BIGINT        NOT NULL,
    [FundingBodyId]             BIGINT        NOT NULL,
    [SessionStartDate]          DATETIME2 (7) NOT NULL,
    [SessionEndDate]            DATETIME2 (7) NOT NULL,
    [SessionTypeId]             BIGINT        NOT NULL,
    [SupportProfessionalUserId] BIGINT        NOT NULL,
    [Status]                    INT           NOT NULL,
    [UserId]                    BIGINT        DEFAULT (CONVERT([bigint],(0))) NOT NULL,
    CONSTRAINT [PK_SupportSession] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SupportSession_AbpUsers_SupportProfessionalUserId] FOREIGN KEY ([SupportProfessionalUserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SupportSession_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]),
    CONSTRAINT [FK_SupportSession_FundingBody_FundingBodyId] FOREIGN KEY ([FundingBodyId]) REFERENCES [dbo].[FundingBody] ([Id]),
    CONSTRAINT [FK_SupportSession_SessionGroup_SessionGroupId] FOREIGN KEY ([SessionGroupId]) REFERENCES [dbo].[SessionGroup] ([Id]),
    CONSTRAINT [FK_SupportSession_SessionType_SessionTypeId] FOREIGN KEY ([SessionTypeId]) REFERENCES [dbo].[SessionType] ([Id]),
    CONSTRAINT [FK_SupportSession_SupportType_SupportTypeId] FOREIGN KEY ([SupportTypeId]) REFERENCES [dbo].[SupportType] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SupportSession_FundingBodyId]
    ON [dbo].[SupportSession]([FundingBodyId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupportSession_SessionGroupId]
    ON [dbo].[SupportSession]([SessionGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupportSession_SessionTypeId]
    ON [dbo].[SupportSession]([SessionTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupportSession_SupportTypeId]
    ON [dbo].[SupportSession]([SupportTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupportSession_SupportProfessionalUserId]
    ON [dbo].[SupportSession]([SupportProfessionalUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupportSession_UserId]
    ON [dbo].[SupportSession]([UserId] ASC);

