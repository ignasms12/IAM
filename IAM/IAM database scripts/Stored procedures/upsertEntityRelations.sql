CREATE PROCEDURE [AD].[upsertEntityRelations]( @destEntity VARCHAR(50), @relationType VARCHAR(50), @userId UNIQUEIDENTIFIER, @content XML)
AS
BEGIN
	DECLARE @rv int = 0
	IF AD.checkPermissions(@userId, CONCAT('AD.', @destEntity, '.upsert')) <> 0
	BEGIN
		PRINT 'No permissions'
		RETURN -1
	END
	IF @destEntity = 'roles'
	BEGIN
		EXEC @rv = [AD].[addFunctionToRole] @content, @userId
	END
	IF @destEntity = 'users'
	BEGIN
		EXEC @rv = [AD].[addRoleToUser] @content, @userId
	END
	IF @rv <> 0
	BEGIN
		PRINT 'Something went wrong'
		RETURN @rv
	END
	RETURN 0
END