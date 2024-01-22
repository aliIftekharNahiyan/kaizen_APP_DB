CREATE TABLE [dbo].[UserWorkRegionLocation] (
    [Id]               BIGINT IDENTITY (1, 1) NOT NULL,
    [UserId]           BIGINT NOT NULL,
    [RegionLocationId] BIGINT NOT NULL,
    CONSTRAINT [PK_UserWorkRegionLocation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserWorkRegionLocation_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserWorkRegionLocation_UserId]
    ON [dbo].[UserWorkRegionLocation]([UserId] ASC);

