CREATE TABLE [dbo].[AcademicYear] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (256) NOT NULL,
    [Description] NVARCHAR (256) NULL,
    [StartDate]   DATETIME2 (7)  NOT NULL,
    [EndDate]     DATETIME2 (7)  NOT NULL,
    [Archived]    BIT            NOT NULL,
    [CompanyId]   BIGINT         NULL,
    CONSTRAINT [PK_AcademicYear] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AcademicYear_Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AcademicYear_CompanyId]
    ON [dbo].[AcademicYear]([CompanyId] ASC);

