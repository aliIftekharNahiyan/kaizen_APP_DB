CREATE TABLE [dbo].[UserClientSupport] (
    [Id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [UserId]      BIGINT        NOT NULL,
    [ClientId]    BIGINT        NOT NULL,
    [IsActive]    BIT           NOT NULL,
    [IsDeleted]   BIT           NOT NULL,
    [CreatedBy]   BIGINT        NOT NULL,
    [CreatedDate] DATETIME2 (7) NOT NULL,
    [UpdatedBy]   BIGINT        NULL,
    [UpdatedDate] DATETIME2 (7) NULL,
    CONSTRAINT [PK_UserClientSupport] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserClientSupport_AbpUsers_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[AbpUsers] ([Id]),
    CONSTRAINT [FK_UserClientSupport_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserClientSupport_ClientId]
    ON [dbo].[UserClientSupport]([ClientId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserClientSupport_UserId]
    ON [dbo].[UserClientSupport]([UserId] ASC);

