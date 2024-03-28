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
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE Users (
Id uniqueidentifier Primary key,
Name varchar(200) NOT NULL,
Email varchar(200) NOT NULL,
CreatedAt Datetime NOT NULL,
ModifiedAt DateTime,
UNIQUE (Name),
UNIQUE (Email)
);
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

CREATE TABLE Orders (
Id uniqueidentifier Primary key,
Status varchar(200) NOT NULL,
UserId uniqueidentifier FOREIGN KEY REFERENCES Users(Id),
CreatedAt Datetime,
ModifiedAt DateTime
);
GO

CREATE TABLE OrderDetails (
Id uniqueidentifier Primary key,
OrderId uniqueidentifier FOREIGN KEY REFERENCES Orders(Id),
ProductId uniqueidentifier FOREIGN KEY REFERENCES Products(Id),
Quantity int 
);
GO