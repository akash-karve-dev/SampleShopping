
IF NOT EXISTS (
    SELECT 1
    FROM sys.databases
    WHERE name = 'OrderDb'
)
BEGIN
    CREATE DATABASE OrderDb;
END;
GO

USE [OrderDb]

-- Users
INSERT INTO Users VALUES ( cast('71479D27-D7A2-4439-99CB-0B536DCEFB45' as uniqueidentifier),'Akash','akash.karve@test.com',GETDATE(),null)

-- Products
INSERT INTO Products VALUES ( cast('43270899-c320-486f-a633-8f0eba7f57ef' as uniqueidentifier),'Table','Furniture',GETDATE(),null)

-- Orders
INSERT INTO Orders VALUES ( cast('c4fd573e-c5bd-463f-878c-dd127a1666df' as uniqueidentifier),'Accepted',cast('71479D27-D7A2-4439-99CB-0B536DCEFB45' as uniqueidentifier),GETDATE(),null)

-- OrderDetails
INSERT INTO OrderDetails VALUES ( cast('7fa59a8b-e57c-4d98-a755-e1530cfad3da' as uniqueidentifier),cast('c4fd573e-c5bd-463f-878c-dd127a1666df' as uniqueidentifier),cast('43270899-c320-486f-a633-8f0eba7f57ef' as uniqueidentifier),1)

