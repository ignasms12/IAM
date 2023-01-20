CREATE TABLE [ID].[sessions]
(
      [id] [UNIQUEIDENTIFIER] NULL CONSTRAINT [DF__sessions__id__5FB337D6] DEFAULT (newid())
    , [userId] [UNIQUEIDENTIFIER] NULL
    , [authCode] [VARBINARY](256) NULL
    , [ipAddress] [NVARCHAR](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
    , [createdTs] [DATETIME] NULL CONSTRAINT [DF__sessions__create__60A75C0F] DEFAULT (getdate())
    , [modifiedTs] [DATETIME] NULL CONSTRAINT [DF__sessions__modifi__619B8048] DEFAULT (getdate())
    , [expirationTs] [DATETIME] NULL
);