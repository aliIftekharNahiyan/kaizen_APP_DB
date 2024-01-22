CREATE TABLE [dbo].[UserSkill] (
    [Id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [UserId]      BIGINT        NOT NULL,
    [SkillId]     BIGINT        NOT NULL,
    [CreatedBy]   BIGINT        DEFAULT (CONVERT([bigint],(0))) NOT NULL,
    [CreatedDate] DATETIME2 (7) DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    [IsActive]    BIT           DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [IsDeleted]   BIT           DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [UpdatedBy]   BIGINT        NULL,
    [UpdatedDate] DATETIME2 (7) NULL,
    CONSTRAINT [PK_UserSkill] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserSkill_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserSkill_Skill_SkillId] FOREIGN KEY ([SkillId]) REFERENCES [dbo].[Skill] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserSkill_UserId]
    ON [dbo].[UserSkill]([UserId] ASC);

