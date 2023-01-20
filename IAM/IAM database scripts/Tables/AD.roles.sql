CREATE TABLE [AD].[roles]
(
      [id] [UNIQUEIDENTIFIER] NOT NULL CONSTRAINT [DF__roles__id__534D60F1] DEFAULT (newid())
    , [name] [NVARCHAR](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
    , [createdTs] [DATETIME] NULL
    , [modifiedTs] [DATETIME] NULL
    , [createdBy] [NVARCHAR](36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
    , [modifiedBy] [NVARCHAR](36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL  
    , CONSTRAINT [PK__roles__3213E83F8895E2D7] PRIMARY KEY CLUSTERED ([id])
);