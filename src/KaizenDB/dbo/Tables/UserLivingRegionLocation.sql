CREATE TABLE [dbo].[UserLivingRegionLocation] (
    [Id]               BIGINT IDENTITY (1, 1) NOT NULL,
    [UserId]           BIGINT NOT NULL,
    [RegionLocationId] BIGINT NOT NULL,
    CONSTRAINT [PK_UserLivingRegionLocation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserLivingRegionLocation_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserLivingRegionLocation_UserId]
    ON [dbo].[UserLivingRegionLocation]([UserId] ASC);

