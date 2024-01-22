CREATE TABLE [dbo].[StorageFile] (
    [Id]            BIGINT          IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (256)  NOT NULL,
    [Description]   NVARCHAR (MAX)  NULL,
    [FileUrl]       NVARCHAR (1024) NOT NULL,
    [FileName]      NVARCHAR (256)  NOT NULL,
    [MimeType]      NVARCHAR (128)  NOT NULL,
    [HasBeenSigned] BIT             NOT NULL,
    [Deleted]       BIT             NOT NULL,
    CONSTRAINT [PK_StorageFile] PRIMARY KEY CLUSTERED ([Id] ASC)
);

