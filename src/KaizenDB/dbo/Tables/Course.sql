CREATE TABLE [dbo].[Course] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (256) NOT NULL,
    [UniversityId] BIGINT         NOT NULL,
    CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Course_University_UniversityId] FOREIGN KEY ([UniversityId]) REFERENCES [dbo].[University] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Course_UniversityId]
    ON [dbo].[Course]([UniversityId] ASC);

