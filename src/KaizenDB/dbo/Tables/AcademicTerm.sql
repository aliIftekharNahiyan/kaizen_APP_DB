CREATE TABLE [dbo].[AcademicTerm] (
    [Id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (256) NOT NULL,
    [Description]    NVARCHAR (512) NULL,
    [StartDate]      DATETIME2 (7)  NOT NULL,
    [EndDate]        DATETIME2 (7)  NOT NULL,
    [Archived]       BIT            NOT NULL,
    [AcademicYearId] BIGINT         NOT NULL,
    CONSTRAINT [PK_AcademicTerm] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AcademicTerm_AcademicYear_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [dbo].[AcademicYear] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AcademicTerm_AcademicYearId]
    ON [dbo].[AcademicTerm]([AcademicYearId] ASC);

