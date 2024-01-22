CREATE TABLE [dbo].[BulkProcessJob] (
    [Id]                BIGINT          IDENTITY (1, 1) NOT NULL,
    [BulkProcessType]   INT             NOT NULL,
    [BulkProcessStatus] INT             NOT NULL,
    [CreatedByUserId]   BIGINT          NOT NULL,
    [TotalRecords]      INT             NOT NULL,
    [SuccessfulRecords] INT             NOT NULL,
    [FailedRecords]     INT             NOT NULL,
    [DateCreated]       DATETIME2 (7)   NOT NULL,
    [DateUpdated]       DATETIME2 (7)   NOT NULL,
    [Cancelled]         BIT             NULL,
    [DateCancelled]     DATETIME2 (7)   NULL,
    [FileUploadedUrl]   NVARCHAR (1024) NOT NULL,
    [FileResultUrl]     NVARCHAR (1024) NULL,
    CONSTRAINT [PK_BulkProcessJob] PRIMARY KEY CLUSTERED ([Id] ASC)
);

