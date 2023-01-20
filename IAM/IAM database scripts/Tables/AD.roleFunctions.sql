CREATE TABLE [AD].[roleFunctions]
(
      [id] [UNIQUEIDENTIFIER] NOT NULL CONSTRAINT [DF_defaultId] DEFAULT (newid())
    , [roleId] [UNIQUEIDENTIFIER] NULL
    , [functionId] [UNIQUEIDENTIFIER] NULL
    , [createdTs] [DATETIME] NULL
    , [modifiedTs] [DATETIME] NULL
    , [createdBy] [NVARCHAR](36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
    , [modifiedBy] [NVARCHAR](36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL  
    , CONSTRAINT [PK__roleFunc__3213E83F913C8073] PRIMARY KEY CLUSTERED ([id])
);