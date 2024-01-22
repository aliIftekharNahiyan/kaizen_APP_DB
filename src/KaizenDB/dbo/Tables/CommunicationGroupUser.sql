CREATE TABLE [dbo].[CommunicationGroupUser] (
    [Id]                   BIGINT IDENTITY (1, 1) NOT NULL,
    [UserId]               BIGINT NOT NULL,
    [CommunicationGroupId] BIGINT NOT NULL,
    CONSTRAINT [PK_CommunicationGroupUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CommunicationGroupUser_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]),
    CONSTRAINT [FK_CommunicationGroupUser_CommunicationGroup_CommunicationGroupId] FOREIGN KEY ([CommunicationGroupId]) REFERENCES [dbo].[CommunicationGroup] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CommunicationGroupUser_CommunicationGroupId]
    ON [dbo].[CommunicationGroupUser]([CommunicationGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CommunicationGroupUser_UserId]
    ON [dbo].[CommunicationGroupUser]([UserId] ASC);

