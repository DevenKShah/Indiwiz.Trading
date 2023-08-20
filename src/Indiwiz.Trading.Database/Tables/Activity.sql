CREATE TABLE [dbo].[Activity]
(
    [Id] BIGINT NOT NULL IDENTITY(1,1), 
    [TimeStamp] DATETIME2 NOT NULL, 
    [ActivityType] VARCHAR(20) NOT NULL, 
    [Amount] DECIMAL(18, 2) NOT NULL, 
    [InstrumentId] BIGINT NULL,
    CONSTRAINT [PK_Activity] PRIMARY KEY ([Id]),
    INDEX [IX_Activity_Type] ([ActivityType], [TimeStamp]),
    INDEX [IX_Activity_Instrument] ([InstrumentId], [ActivityType], [TimeStamp])
)
