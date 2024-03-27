
IF NOT EXISTS (
    SELECT 1
    FROM sys.databases
    WHERE name = 'UserDb'
)
BEGIN
    CREATE DATABASE UserDb;
END;
GO

USE [UserDb]
GO

IF EXISTS (
    SELECT 1 
    FROM sys.tables 
    WHERE name = 'Users' 
    AND type = 'U'
)
BEGIN
    -- Table exists, you can add your logic here
    INSERT INTO Users VALUES ( cast('71479D27-D7A2-4439-99CB-0B536DCEFB45' as uniqueidentifier),'Akash','akash.karve@test.com',GETDATE(),null)
END



