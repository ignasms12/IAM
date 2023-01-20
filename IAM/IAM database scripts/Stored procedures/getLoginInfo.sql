;create PROCEDURE [ID].[getLoginInfo]( @username nvarchar(30), @ipAddress nvarchar(20))ASBEGIN IF (

SELECT  COUNT(*)
FROM AD.users
WHERE username = @username) <> 1 BEGIN
SELECT  [statusMessage] = 'User not found with given username' RETURN -1 END
SELECT  [id]
       ,[username]
       ,[password]
       ,[email]
       ,[statusMessage] = 'User found'
FROM AD.users
WHERE username = @username RETURN 0END; 