;CREATE PROCEDURE [AD].[upsertRole]( @content XML, @userId UNIQUEIDENTIFIER)ASBEGIN DECLARE @objectExists int; DECLARE @conflict int DECLARE @newRoleIds TABLE (id UNIQUEIDENTIFIER); DECLARE @name NVARCHAR(128) DECLARE @id UNIQUEIDENTIFIER DECLARE @action VARCHAR(30) DECLARE @o_name NVARCHAR(128) DECLARE @now DATETIME = GETDATE() IF @content IS NULL BEGIN

SELECT  'Invalid parameters provided' AS [message]; RETURN -1; END

SET @name = @content.value('(/Role/Name)[1]', 'nvarchar(128)')
SET @id = @content.value('(/Role/Id)[1]', 'uniqueidentifier') IF @name IS NULL AND (@id IS NULL AND @action = 'DELETE') BEGIN
SELECT  'Invalid parameters provided' AS [message]; RETURN -1; END

SET @objectExists = (
SELECT  COUNT(*)
FROM AD.roles
WHERE id = @id)

SET @conflict = (
SELECT  COUNT(*)
FROM AD.roles
WHERE id <> @id
AND [name] = @name) IF @objectExists > 0 AND @action = 'UPDATE' BEGIN IF @conflict = 0 BEGIN
SELECT  'Role with this name already exists' AS [message] RETURN -2 END ELSE BEGIN
SELECT  @o_name = [name]
FROM AD.roles
WHERE id = @id IF @name IS NULL

SET @name = @o_name UPDATE [AD].[roles]
SET [name] = @name, [modifiedBy] = @userId, [modifiedTs] = @now
WHERE [id] = @id
SELECT  CONCAT('Role ',@name,' updated') AS [message]; END END IF @objectExists > 0 AND @action = 'DELETE' BEGIN DELETE
FROM AD.roles
WHERE id = @id
SELECT  'Role has been deleted' AS [message] END IF @objectExists = 0 AND @action = 'INSERT' BEGIN

INSERT INTO AD.roles([name], [createdTs], [modifiedTs], [createdBy], [modifiedBy]) OUTPUT inserted.id INTO @newRoleIds VALUES(@name, @now, @now, @userId, @userId)
SELECT  id
       ,'New role created' AS [message]
FROM @newRoleIds END ELSE BEGIN
SELECT  'Desired action cannot be completed for this entity' AS [message] RETURN -2; ENDEND;

