;CREATE PROCEDURE [AD].[getFunctions]( @userId UNIQUEIDENTIFIER, @statusMessage VARCHAR(60) OUTPUT)ASBEGIN IF AD.checkPermissions(@userId, 'AD.users.all') <> 0 BEGIN

SET @statusMessage = 'No permissions' RETURN -1; END IF AD.checkPermissions(@userId, 'AD.functions.view') <> 0 BEGIN
SET @statusMessage = 'No permissions' RETURN -1; END

SELECT  [id]
       ,[name]
       ,[description]
       ,[createdTs]
       ,[createdBy]
       ,[modifiedTs]
       ,[modifiedBy]
FROM AD.functionsEND;

