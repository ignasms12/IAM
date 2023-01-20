;CREATE PROCEDURE [AD].[upsertUser]( @content XML, @userId UNIQUEIDENTIFIER)ASBEGIN DECLARE @objectExists int; DECLARE @conflict int; DECLARE @newUserIds TABLE (id UNIQUEIDENTIFIER); DECLARE @username NVARCHAR(30), @password NVARCHAR(514), @email NVARCHAR(100), @id UNIQUEIDENTIFIER, @action VARCHAR(30) DECLARE @o_username NVARCHAR(30), @o_password NVARCHAR(514), @o_email NVARCHAR(100) DECLARE @now DATETIME = GETDATE() IF @content IS NULL BEGIN

SELECT  'Invalid parameters provided' AS [message]; RETURN -1; END

SET @username = @content.value('(/User/Username)[1]', 'nvarchar(30)')
SET @password = @content.value('(/User/Password)[1]', 'nvarchar(514)')
SET @email = @content.value('(/User/Email)[1]', 'nvarchar(100)')
SET @id = @content.value('(/User/Id)[1]', 'uniqueidentifier')
SET @action = @content.value('(/User/Action)[1]', 'varchar(30)') IF @username IS NULL AND @password IS NULL AND @email IS NULL AND (@id IS NULL AND @action = 'DELETE') BEGIN
SELECT  'Invalid parameters provided' AS [message]; RETURN -1; END

SET @objectExists = (
SELECT  COUNT(*)
FROM AD.users
WHERE id = @id)

SET @conflict = (
SELECT  COUNT(*)
FROM AD.users
WHERE id <> @id
AND (username = @username OR email = @email)) IF @objectExists > 0 AND @action = 'UPDATE' BEGIN
SELECT  @o_username = [username]
       ,@o_password = [password]
       ,@o_email = [email]
FROM AD.users
WHERE id = @id IF @conflict = 0 BEGIN
SELECT  'User with this username/email already exists' AS [message] RETURN -2; END ELSE BEGIN IF @username IS NULL

SET @username = @o_username IF @password IS NULL
SET @password = @o_password IF @email IS NULL
SET @email = @o_email UPDATE [AD].[users]
SET [username] = @username, [password] = @password, [email] = @email, [modifiedBy] = @userId, [modifiedTs] = @now
WHERE [id] = @id
SELECT  CONCAT('User ',@username,' updated') AS [message]; END END IF @objectExists > 0 AND @action = 'DELETE' BEGIN DELETE
FROM AD.users
WHERE id = @id
SELECT  'User has been deleted' AS [message] END IF @objectExists = 0 AND @action = 'INSERT' BEGIN

INSERT INTO AD.users([username], [password], [email], [createdTs], [modifiedTs], [createdBy], [modifiedBy]) OUTPUT Inserted.id INTO @newUserIds VALUES(@username, @password, @email, @now, @now, @userId, @userId)
SELECT  id
       ,'New user created' AS [message]
FROM @newUserIds END ELSE BEGIN
SELECT  'Desired action cannot be completed for this entity' AS [message] RETURN -2; END END;;

