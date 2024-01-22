CREATE TABLE [dbo].[UserAddress] (
    [Id]        BIGINT IDENTITY (1, 1) NOT NULL,
    [UserId]    BIGINT NOT NULL,
    [AddressId] BIGINT NOT NULL,
    CONSTRAINT [PK_UserAddress] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserAddress_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserAddress_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserAddress_AddressId]
    ON [dbo].[UserAddress]([AddressId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserAddress_UserId]
    ON [dbo].[UserAddress]([UserId] ASC);

