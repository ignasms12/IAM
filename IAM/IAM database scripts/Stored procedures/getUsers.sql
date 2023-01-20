;create PROCEDURE [AD].[getUsers]( @userId UNIQUEIDENTIFIER, @statusMessage varchar(60) OUTPUT)ASBEGIN IF AD.checkPermissions(@userId, 'AD.users.all') <> 0 BEGIN

SET @statusMessage = 'No permissions' RETURN -1; END IF AD.checkPermissions(@userId, 'AD.users.view') <> 0 BEGIN
SET @statusMessage = 'No permissions' RETURN -1; END

SELECT  id
       ,username
       ,email
       ,createdTs
       ,createdBy
       ,modifiedTs
       ,modifiedBy
FROM AD.usersEND;

