;create PROCEDURE [AD].[addFunctionToRole]( @content XML, @userId UNIQUEIDENTIFIER)ASBEGIN DECLARE @roleId UNIQUEIDENTIFIER DECLARE @functions TABLE(id UNIQUEIDENTIFIER) DECLARE @action VARCHAR(30) DECLARE @now DATETIME = GETDATE()

SET @roleId = @content.[value]('(/Change/RoleId)[1]', 'UNIQUEIDENTIFIER')
SET @action = @content.[value]('(/Change/Action)[1]', 'VARCHAR(30)')
INSERT INTO @functions(id)
SELECT  T.Col.[value]('.','UNIQUEIDENTIFIER')
FROM @content.nodes
('/Change/Functions/FunctionId'
) T(Col) IF @roleId IS NULL OR @action IS NULL BEGIN
SELECT  'Invalid parameters' AS [message]; RETURN -1; END IF @action = 'INSERT' BEGIN
INSERT INTO AD.roleFunctions(functionId, roleId, createdBy, modifiedBy, createdTs, modifiedTs)
SELECT  id
       ,@roleId
       ,@userId
       ,@userId
       ,@now
       ,@now
FROM @functions
SELECT  'Function(s) assigned to role successfully' AS [message]; END IF @action = 'DELETE' BEGIN
DELETE rf
FROM AD.roleFunctions rf
INNER JOIN @functions f
ON rf.functionId = f.id AND rf.roleId = @roleId
SELECT  'Function(s) removed from role successfully' AS [message]; END END;

