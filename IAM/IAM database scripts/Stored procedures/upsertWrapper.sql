CREATE PROCEDURE [AD].[upsertWrapper]( @entityType VARCHAR(50), @userId UNIQUEIDENTIFIER, @content XML)
AS
BEGIN
	DECLARE @rv int = 0
	IF AD.checkPermissions(@userId, CONCAT('AD.', @entityType, '.upsert')) <> 0
	BEGIN
		PRINT 'No permissions'
		RETURN -1
	END
	IF @entityType = 'functions'
	BEGIN
		EXEC @rv = [AD].[upsertFunction] @content, @userId
	END
	IF @entityType = 'roles'
	BEGIN
		EXEC @rv = [AD].[upsertRole] @content, @userId
	END
	IF @entityType = 'users'
	BEGIN
		EXEC @rv = [AD].[upsertUser] @content, @userId
	END
	IF @rv <> 0
	BEGIN
		PRINT 'Something went wrong'
		RETURN @rv
	END
	RETURN 0
END