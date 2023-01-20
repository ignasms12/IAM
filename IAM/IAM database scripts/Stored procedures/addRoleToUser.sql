;CREATE PROCEDURE [AD].[addRoleToUser]( @content XML, @userId UNIQUEIDENTIFIER)ASBEGIN DECLARE @targetUserId UNIQUEIDENTIFIER DECLARE @roles TABLE(id UNIQUEIDENTIFIER) DECLARE @action VARCHAR(30) DECLARE @now DATETIME = GETDATE()

SET @targetUserId = @content.[value]('(/Change/TargetUserId)[1]', 'UNIQUEIDENTIFIER')
SET @action = @content.[value]('(/Change/Action)[1]', 'VARCHAR(30)')
INSERT INTO @roles(id)
SELECT  T.Col.[value]('.','UNIQUEIDENTIFIER')
FROM @content.nodes
('/Change/Roles/RoleId'
) T(Col) IF @targetUserId IS NULL OR @action IS NULL BEGIN
SELECT  'Invalid parameters' AS [message]; RETURN -1; END IF @action = 'INSERT' BEGIN
INSERT INTO AD.userRoles(roleId, userId, createdBy, modifiedBy, createdTs, modifiedTs)
SELECT  id
       ,@targetUserId
       ,@userId
       ,@userId
       ,@now
       ,@now
FROM @roles END IF @action = 'DELETE' BEGIN
DELETE ur
FROM AD.userRoles ur
INNER JOIN @roles r
ON ur.roleId = r.id AND ur.userId = @targetUserId END
SELECT  'Function assigned to role successfully' AS [message];END;

