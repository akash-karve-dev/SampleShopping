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

CREATE TABLE InboxState (
	Id bigint IDENTITY(1,1) NOT NULL,
	MessageId uniqueidentifier NOT NULL,
	ConsumerId uniqueidentifier NOT NULL,
	LockId uniqueidentifier NOT NULL,
	RowVersion timestamp NULL,
	Received datetime2 NOT NULL,
	ReceiveCount int NOT NULL,
	ExpirationTime datetime2 NULL,
	Consumed datetime2 NULL,
	Delivered datetime2 NULL,
	LastSequenceNumber bigint NULL,
	CONSTRAINT AK_InboxState_MessageId_ConsumerId UNIQUE (MessageId,ConsumerId),
	CONSTRAINT PK_InboxState PRIMARY KEY (Id)
);
 CREATE NONCLUSTERED INDEX IX_InboxState_Delivered ON InboxState (  Delivered ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ]
GO

CREATE TABLE OutboxMessage (
	SequenceNumber bigint IDENTITY(1,1) NOT NULL,
	EnqueueTime datetime2 NULL,
	SentTime datetime2 NOT NULL,
	Headers nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Properties nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	InboxMessageId uniqueidentifier NULL,
	InboxConsumerId uniqueidentifier NULL,
	OutboxId uniqueidentifier NULL,
	MessageId uniqueidentifier NOT NULL,
	ContentType nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	MessageType nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Body nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ConversationId uniqueidentifier NULL,
	CorrelationId uniqueidentifier NULL,
	InitiatorId uniqueidentifier NULL,
	RequestId uniqueidentifier NULL,
	SourceAddress nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DestinationAddress nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ResponseAddress nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	FaultAddress nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ExpirationTime datetime2 NULL,
	CONSTRAINT PK_OutboxMessage PRIMARY KEY (SequenceNumber)
);
 CREATE NONCLUSTERED INDEX IX_OutboxMessage_EnqueueTime ON OutboxMessage (  EnqueueTime ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ];
 CREATE NONCLUSTERED INDEX IX_OutboxMessage_ExpirationTime ON dbo.OutboxMessage (  ExpirationTime ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ];
 CREATE  UNIQUE NONCLUSTERED INDEX IX_OutboxMessage_InboxMessageId_InboxConsumerId_SequenceNumber ON dbo.OutboxMessage (  InboxMessageId ASC  , InboxConsumerId ASC  , SequenceNumber ASC  )  
	 WHERE  ([InboxMessageId] IS NOT NULL AND [InboxConsumerId] IS NOT NULL)
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ];
 CREATE  UNIQUE NONCLUSTERED INDEX IX_OutboxMessage_OutboxId_SequenceNumber ON dbo.OutboxMessage (  OutboxId ASC  , SequenceNumber ASC  )  
	 WHERE  ([OutboxId] IS NOT NULL)
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ];
GO

CREATE TABLE OutboxState (
	OutboxId uniqueidentifier NOT NULL,
	LockId uniqueidentifier NOT NULL,
	RowVersion timestamp NULL,
	Created datetime2 NOT NULL,
	Delivered datetime2 NULL,
	LastSequenceNumber bigint NULL,
	CONSTRAINT PK_OutboxState PRIMARY KEY (OutboxId)
);
 CREATE NONCLUSTERED INDEX IX_OutboxState_Created ON OutboxState (  Created ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ];
GO
