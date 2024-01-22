CREATE TABLE [dbo].[CommunicationTemplate] (
    [Id]                     BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]                   NVARCHAR (256) NOT NULL,
    [HeaderTemplateId]       BIGINT         NULL,
    [FooterTemplateId]       BIGINT         NULL,
    [TemplateHTMLContentUrl] NVARCHAR (MAX) NULL,
    [TemplateType]           INT            NOT NULL,
    [Deleted]                BIT            NOT NULL,
    [CompanyId]              BIGINT         NULL,
    [TemplateArea]           INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CommunicationTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CommunicationTemplate_CommunicationTemplate_FooterTemplateId] FOREIGN KEY ([FooterTemplateId]) REFERENCES [dbo].[CommunicationTemplate] ([Id]),
    CONSTRAINT [FK_CommunicationTemplate_CommunicationTemplate_HeaderTemplateId] FOREIGN KEY ([HeaderTemplateId]) REFERENCES [dbo].[CommunicationTemplate] ([Id]),
    CONSTRAINT [FK_CommunicationTemplate_Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CommunicationTemplate_FooterTemplateId]
    ON [dbo].[CommunicationTemplate]([FooterTemplateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CommunicationTemplate_HeaderTemplateId]
    ON [dbo].[CommunicationTemplate]([HeaderTemplateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CommunicationTemplate_CompanyId]
    ON [dbo].[CommunicationTemplate]([CompanyId] ASC);

