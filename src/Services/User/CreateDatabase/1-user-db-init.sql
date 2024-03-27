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