CREATE TABLE [AD].[userRoles]
(
      [id] [UNIQUEIDENTIFIER] NULL CONSTRAINT [DF__userRoles__id__5629CD9C] DEFAULT (newid())
    , [userId] [UNIQUEIDENTIFIER] NOT NULL
    , [roleId] [UNIQUEIDENTIFIER] NOT NULL
    , [createdBy] [NVARCHAR](36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
    , [modifiedBy] [NVARCHAR](36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
    , [createdTs] [DATETIME] NULL CONSTRAINT [DF_createdTs] DEFAULT (getdate())
    , [modifiedTs] [DATETIME] NULL CONSTRAINT [DF_modifiedTs] DEFAULT (getdate())
);