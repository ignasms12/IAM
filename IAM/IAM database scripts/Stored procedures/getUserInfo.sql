;CREATE PROCEDURE [AD].[getUserInfo]( @userId uniqueidentifier, @targetUserId uniqueidentifier, @mode nvarchar(40), @statusMessage nvarchar(60) OUTPUT)ASBEGIN IF @targetUserId IS NULL BEGIN

SET @statusMessage = 'Invalid parameters provided' RETURN -1; END IF ISJSON(@mode) = 0 BEGIN
SET @statusMessage = 'Incorrect json provided' RETURN -2; END IF 1 = (

SELECT  1
FROM OPENJSON
(@mode
)
WHERE [value] = 'basic') -- GET basic user info BEGIN IF AD.checkPermissions(@userId, 'AD.users.view') <> 0 AND @userId <> @targetUserId BEGIN

SET @statusMessage = 'No permissions' RETURN -1; END

SELECT  [id] , [username] , [email] , [createdTs] , [modifiedTs] , [createdBy] , [modifiedBy]
FROM [AD].[users]
WHERE id = @targetUserId END IF 1 = (
SELECT  1
FROM OPENJSON(@mode)
WHERE [value] = 'roles') -- GET user roles BEGIN IF AD.checkPermissions(@userId, 'AD.roles.view') <> 0 OR (AD.checkPermissions(@userId, 'AD.users.view') <> 0
AND @userId <> @targetUserId) BEGIN

SET @statusMessage = 'No permissions' RETURN -1; END

SELECT  r.[id] , r.[name] , r.[createdTs] , r.[modifiedTs] , r.[createdBy] , r.[modifiedBy]
FROM [AD].[roles] r
INNER JOIN [AD].[userRoles] ur
ON r.[id] = ur.[roleId]
WHERE ur.[userId] = @targetUserId END IF 1 = (
SELECT  1
FROM OPENJSON(@mode)
WHERE [value] = 'functions') -- GET user functions BEGIN IF AD.checkPermissions(@userId, 'AD.functions.view') <> 0 OR (AD.checkPermissions(@userId, 'AD.users.view') <> 0
AND @userId <> @targetUserId) BEGIN

SET @statusMessage = 'No permissions' RETURN -1; END

SELECT  f.[id] , f.[name] , f.[description] , f.[createdTs] , f.[modifiedTs] , f.[createdBy] , f.[modifiedBy]
FROM [AD].[userRoles] ur
INNER JOIN [AD].[roleFunctions] rf
ON ur.[roleId] = rf.[roleId]
INNER JOIN [AD].[functions] f
ON rf.[functionId] = f.[id]
WHERE ur.[userId] = @targetUserId ENDEND;

