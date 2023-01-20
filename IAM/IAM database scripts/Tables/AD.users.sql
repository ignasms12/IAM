CREATE TABLE [AD].[users]
(
      [id] [UNIQUEIDENTIFIER] NOT NULL CONSTRAINT [DF__users__id__5CD6CB2B] DEFAULT (newid())
    , [username] [NVARCHAR](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
    , [password] [NVARCHAR](514) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
    , [email] [NVARCHAR](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
    , [createdTs] [DATETIME] NULL
    , [modifiedTs] [DATETIME] NULL
    , [createdBy] [NVARCHAR](36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
    , [modifiedBy] [NVARCHAR](36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL  
    , CONSTRAINT [PK__users__3213E83FC73C91DA] PRIMARY KEY CLUSTERED ([id])
);