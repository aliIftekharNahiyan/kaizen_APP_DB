CREATE TABLE [dbo].[CourseTerm] (
    [Id]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [LengthOfTerm] INT           NOT NULL,
    [StartDate]    DATETIME2 (7) NULL,
    [EndDate]      DATETIME2 (7) NULL,
    [CourseId]     BIGINT        NOT NULL,
    CONSTRAINT [PK_CourseTerm] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CourseTerm_Course_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Course] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_CourseTerm_CourseId]
    ON [dbo].[CourseTerm]([CourseId] ASC);

