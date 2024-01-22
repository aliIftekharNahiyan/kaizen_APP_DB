CREATE TABLE [dbo].[RegionLocation] (
    [Id]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (256) NOT NULL,
    [RegionId] BIGINT         NOT NULL,
    [Deleted]  BIT            NOT NULL,
    CONSTRAINT [PK_RegionLocation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RegionLocation_Region_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [dbo].[Region] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_RegionLocation_RegionId]
    ON [dbo].[RegionLocation]([RegionId] ASC);

