
IF NOT EXISTS (
    SELECT 1
    FROM sys.databases
    WHERE name = 'InventoryDB'
)
BEGIN
    CREATE DATABASE InventoryDB;
END;
GO

USE [InventoryDB]
GO

-- Products
INSERT INTO Products VALUES ( cast('43270899-c320-486f-a633-8f0eba7f57ef' as uniqueidentifier),'Table','Furniture',GETDATE(),null)
GO


-- Inventories
INSERT INTO Inventories VALUES ( cast('75a0d737-02b0-47d2-a983-addc5d717fde' as uniqueidentifier), cast('43270899-c320-486f-a633-8f0eba7f57ef' as uniqueidentifier),10,GETDATE(),null)
GO


