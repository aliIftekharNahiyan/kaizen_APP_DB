CREATE TABLE [dbo].[AbpUsers] (
    [Id]                             BIGINT         IDENTITY (1, 1) NOT NULL,
    [AccessFailedCount]              INT            NOT NULL,
    [AuthenticationSource]           NVARCHAR (64)  NULL,
    [ConcurrencyStamp]               NVARCHAR (128) NULL,
    [CreationTime]                   DATETIME2 (7)  NOT NULL,
    [CreatorUserId]                  BIGINT         NULL,
    [DeleterUserId]                  BIGINT         NULL,
    [DeletionTime]                   DATETIME2 (7)  NULL,
    [EmailAddress]                   NVARCHAR (256) NOT NULL,
    [EmailConfirmationCode]          NVARCHAR (328) NULL,
    [IsActive]                       BIT            NOT NULL,
    [IsDeleted]                      BIT            NOT NULL,
    [IsEmailConfirmed]               BIT            NOT NULL,
    [IsLockoutEnabled]               BIT            NOT NULL,
    [IsPhoneNumberConfirmed]         BIT            NOT NULL,
    [IsTwoFactorEnabled]             BIT            NOT NULL,
    [LastModificationTime]           DATETIME2 (7)  NULL,
    [LastModifierUserId]             BIGINT         NULL,
    [LockoutEndDateUtc]              DATETIME2 (7)  NULL,
    [Name]                           NVARCHAR (64)  NOT NULL,
    [NormalizedEmailAddress]         NVARCHAR (256) NOT NULL,
    [NormalizedUserName]             NVARCHAR (256) NOT NULL,
    [Password]                       NVARCHAR (128) NOT NULL,
    [PasswordResetCode]              NVARCHAR (328) NULL,
    [PhoneNumber]                    NVARCHAR (32)  NULL,
    [SecurityStamp]                  NVARCHAR (128) NULL,
    [Surname]                        NVARCHAR (64)  NOT NULL,
    [TenantId]                       INT            NULL,
    [UserName]                       NVARCHAR (256) NOT NULL,
    [AgreePrivacyPolicy]             BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [UniversityId]                   BIGINT         NULL,
    [DoNotContact]                   BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [MarketingConsent]               BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [PaymentTermId]                  BIGINT         NULL,
    [Pronouns]                       NVARCHAR (128) NULL,
    [StoreDetailsConsent]            BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Website]                        NVARCHAR (512) NULL,
    [CustomerTypeId]                 BIGINT         NULL,
    [LastLoginTime]                  DATETIME2 (7)  NULL,
    [UserTypeId]                     BIGINT         NULL,
    [PronounId]                      BIGINT         NULL,
    [VisibilityPermissionId]         BIGINT         NULL,
    [MedicalHistory]                 NVARCHAR (128) NULL,
    [Archived]                       BIT            NULL,
    [CreatedBy]                      BIGINT         DEFAULT (CONVERT([bigint],(0))) NOT NULL,
    [CreatedDate]                    DATETIME2 (7)  DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    [DateofBirth]                    DATETIME2 (7)  NULL,
    [DpStatus]                       BIT            NULL,
    [ParentFamilyMember]             BIGINT         NULL,
    [PreferredCommunicationLanguage] NVARCHAR (36)  NULL,
    [StartDate]                      DATETIME2 (7)  NULL,
    [UpdatedBy]                      BIGINT         NULL,
    [UpdatedDate]                    DATETIME2 (7)  NULL,
    [AgreeTC]                        BIT            NULL,
    [FirstName] NVARCHAR(128) NULL, 
    CONSTRAINT [PK_AbpUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AbpUsers_AbpUsers_CreatorUserId] FOREIGN KEY ([CreatorUserId]) REFERENCES [dbo].[AbpUsers] ([Id]),
    CONSTRAINT [FK_AbpUsers_AbpUsers_DeleterUserId] FOREIGN KEY ([DeleterUserId]) REFERENCES [dbo].[AbpUsers] ([Id]),
    CONSTRAINT [FK_AbpUsers_AbpUsers_LastModifierUserId] FOREIGN KEY ([LastModifierUserId]) REFERENCES [dbo].[AbpUsers] ([Id]),
    CONSTRAINT [FK_AbpUsers_CustomerType_CustomerTypeId] FOREIGN KEY ([CustomerTypeId]) REFERENCES [dbo].[CustomerType] ([Id]),
    CONSTRAINT [FK_AbpUsers_Lookup_PronounId] FOREIGN KEY ([PronounId]) REFERENCES [dbo].[Lookup] ([Id]),
    CONSTRAINT [FK_AbpUsers_Lookup_UserTypeId] FOREIGN KEY ([UserTypeId]) REFERENCES [dbo].[Lookup] ([Id]),
    CONSTRAINT [FK_AbpUsers_Lookup_VisibilityPermissionId] FOREIGN KEY ([VisibilityPermissionId]) REFERENCES [dbo].[Lookup] ([Id]),
    CONSTRAINT [FK_AbpUsers_PaymentTerm_PaymentTermId] FOREIGN KEY ([PaymentTermId]) REFERENCES [dbo].[PaymentTerm] ([Id]),
    CONSTRAINT [FK_AbpUsers_University_UniversityId] FOREIGN KEY ([UniversityId]) REFERENCES [dbo].[University] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUsers_CreatorUserId]
    ON [dbo].[AbpUsers]([CreatorUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUsers_DeleterUserId]
    ON [dbo].[AbpUsers]([DeleterUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUsers_LastModifierUserId]
    ON [dbo].[AbpUsers]([LastModifierUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUsers_TenantId_NormalizedEmailAddress]
    ON [dbo].[AbpUsers]([TenantId] ASC, [NormalizedEmailAddress] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUsers_TenantId_NormalizedUserName]
    ON [dbo].[AbpUsers]([TenantId] ASC, [NormalizedUserName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUsers_PaymentTermId]
    ON [dbo].[AbpUsers]([PaymentTermId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUsers_CustomerTypeId]
    ON [dbo].[AbpUsers]([CustomerTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUsers_UniversityId]
    ON [dbo].[AbpUsers]([UniversityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUsers_PronounId]
    ON [dbo].[AbpUsers]([PronounId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUsers_UserTypeId]
    ON [dbo].[AbpUsers]([UserTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AbpUsers_VisibilityPermissionId]
    ON [dbo].[AbpUsers]([VisibilityPermissionId] ASC);

