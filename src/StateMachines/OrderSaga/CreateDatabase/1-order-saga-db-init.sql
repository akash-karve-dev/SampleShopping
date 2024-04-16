IF NOT EXISTS (
    SELECT 1
    FROM sys.databases
    WHERE name = 'OrderSagaDb'
)
BEGIN
    CREATE DATABASE OrderSagaDb;
END;
GO

USE [OrderSagaDb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE OrderSagaDb.dbo.OrderStateInstance (
	CorrelationId uniqueidentifier NOT NULL,
	CurrentState nvarchar(64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	OrderId uniqueidentifier NOT NULL,
	UserId uniqueidentifier NOT NULL,
	CreatedAt Datetime NOT NULL,
	CONSTRAINT PK_OrderStateInstance PRIMARY KEY (CorrelationId)
);