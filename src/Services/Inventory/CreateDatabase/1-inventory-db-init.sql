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

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE Products (
Id uniqueidentifier Primary key,
Name varchar(200) NOT NULL,
Category varchar(200) NOT NULL,
CreatedAt Datetime NOT NULL,
ModifiedAt DateTime,
UNIQUE (Name)
);
GO


CREATE TABLE Inventories (
Id uniqueidentifier Primary key,
ProductId uniqueidentifier FOREIGN KEY REFERENCES Products(Id),
Quantity int NOT NULL,
CreatedAt Datetime NOT NULL,
ModifiedAt DateTime
);
GO

