CREATE TABLE [dbo].[PaymentTerm] (
    [Id]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NOT NULL,
    [Deleted] BIT            NOT NULL,
    CONSTRAINT [PK_PaymentTerm] PRIMARY KEY CLUSTERED ([Id] ASC)
);

