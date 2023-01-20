;CREATE PROCEDURE [AD].[upsertFunction]( @content XML, @userId UNIQUEIDENTIFIER)ASBEGIN DECLARE @objectExists INT; DECLARE @conflict INT DECLARE @newFunctionIds TABLE (id UNIQUEIDENTIFIER); DECLARE @id UNIQUEIDENTIFIER, @name NVARCHAR(256), @description NVARCHAR(1024), @action VARCHAR(30) DECLARE @o_name NVARCHAR(256), @o_description NVARCHAR(1024) IF @content IS NULL BEGIN

SELECT  'Invalid parameters provided' AS [message]; RETURN -1; END

SET @name = @content.value('(/Function/Name)[1]', 'nvarchar(256)')
SET @description = @content.value('(/Function/Description)[1]', 'nvarchar(1024)')
SET @action = @content.value('(/Function/Action)[1]', 'varchar(30)')
SET @id = @content.value('(/Function/Id)[1]', 'uniqueidentifier') IF @name IS NULL AND @description IS NULL AND (@id IS NULL AND @action = 'DELETE') BEGIN
SELECT  'Invalid parameters provided' AS [message]; RETURN -1; END

SET @objectExists = (
SELECT  COUNT(*)
FROM AD.functions
WHERE [id] = @id)

SET @conflict = (
SELECT  COUNT(*)
FROM AD.functions
WHERE [id] <> @id
AND [name] = @name) IF @objectExists > 0 AND @action = 'UPDATE' BEGIN IF @conflict = 0 BEGIN
SELECT  'Function with this name already exists' AS [message] RETURN -2; END ELSE BEGIN

SELECT  @o_name = [name]
       ,@o_description = [description]
FROM AD.functions
WHERE id = @id IF @name IS NULL

SET @name = @o_name IF @description IS NULL
SET @description = @o_description UPDATE [AD].[functions]
SET [description] = @description, [name] = @name, [modifiedBy] = @userId
WHERE id = @id
SELECT  CONCAT('Function ',@name,' updated') AS [message]; END END IF @objectExists > 0 AND @action = 'DELETE' BEGIN DELETE
FROM AD.functions
WHERE id = @id
SELECT  'Function has been deleted' AS [message] END IF @objectExists = 0 AND @action = 'INSERT' BEGIN

INSERT INTO AD.functions([name], [description], [createdBy], [modifiedBy]) OUTPUT inserted.id INTO @newFunctionIds VALUES(@name, @description, @userId, @userId)
SELECT  id
       ,'New function created' AS [message]
FROM @newFunctionIds END ELSE BEGIN
SELECT  'Desired action cannot be completed for this entity' AS [message] RETURN -2; END END;

