;CREATE PROCEDURE [AD].[getFunctionInfo]( @userId uniqueidentifier, @targetFunctionId uniqueidentifier, @mode nvarchar(40), @statusMessage nvarchar(60) OUTPUT)ASBEGIN IF @targetFunctionId IS NULL BEGIN

SET @statusMessage = 'Invalid parameters provided' RETURN -1; END IF ISJSON(@mode) = 0 BEGIN
SET @statusMessage = 'Incorrect json provided' RETURN -2; END IF 1 = (

SELECT  1
FROM OPENJSON
(@mode
)
WHERE [value] = 'basic') -- GET basic function info BEGIN IF AD.checkPermissions(@userId, 'AD.functions.view') <> 0 BEGIN

SET @statusMessage = 'No permissions' RETURN -3; END

SELECT  [id] , [name] , [description] , [createdTs] , [modifiedTs] , [createdBy] , [modifiedBy]
FROM [AD].[functions]
WHERE id = @targetFunctionId END IF 1 = (
SELECT  1
FROM OPENJSON(@mode)
WHERE [value] = 'roles') -- GET function roles BEGIN IF AD.checkPermissions(@userId, 'AD.roles.view') <> 0 OR AD.checkPermissions(@userId, 'AD.functions.view') <> 0 BEGIN

SET @statusMessage = 'No permissions' RETURN -3; END

SELECT  r.[id] , r.[name]
FROM [AD].[roles] r
INNER JOIN [AD].[roleFunctions] rf
ON r.[id] = rf.[roleId]
WHERE rf.[functionId] = @targetFunctionId END IF 1 = (
SELECT  1
FROM OPENJSON(@mode)
WHERE [value] = 'users') -- GET function users BEGIN IF AD.checkPermissions(@userId, 'AD.functions.view') <> 0 OR AD.checkPermissions(@userId, 'AD.users.view') <> 0 OR AD.checkPermissions(@userId, 'AD.roles.view') <> 0 BEGIN

SET @statusMessage = 'No permissions' RETURN -3; END

SELECT  u.[id] , u.[username] , u.[email] , r.[id] AS roleId , r.[name] AS roleName
FROM [AD].[users] u
INNER JOIN [AD].[userRoles] ur
ON u.[id] = ur.[userId]
INNER JOIN [AD].[roles] r
ON ur.[roleId] = r.[id]
INNER JOIN [AD].[roleFunctions] rf
ON r.[id] = rf.[roleId]
WHERE rf.[functionId] = @targetFunctionId ENDEND;

