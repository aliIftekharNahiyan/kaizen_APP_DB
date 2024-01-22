CREATE TABLE [dbo].[UserDisability] (
    [Id]           BIGINT IDENTITY (1, 1) NOT NULL,
    [UserId]       BIGINT NOT NULL,
    [DisabilityId] BIGINT NOT NULL,
    CONSTRAINT [PK_UserDisability] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserDisability_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserDisability_UserId]
    ON [dbo].[UserDisability]([UserId] ASC);

