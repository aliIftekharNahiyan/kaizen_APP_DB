CREATE TABLE [dbo].[PhoneNumber] (
    [Id]              BIGINT IDENTITY (1, 1) NOT NULL,
    [Number]          INT    NOT NULL,
    [PhoneNumberType] INT    NOT NULL,
    [IsPrimary]       BIT    NOT NULL,
    [UserId]          BIGINT NOT NULL,
    CONSTRAINT [PK_PhoneNumber] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PhoneNumber_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_PhoneNumber_UserId]
    ON [dbo].[PhoneNumber]([UserId] ASC);

