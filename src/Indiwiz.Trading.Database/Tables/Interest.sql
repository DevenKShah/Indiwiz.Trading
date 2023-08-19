CREATE TABLE [dbo].[Interest]
(
    [Id] BIGINT NOT NULL IDENTITY(1,1), 
    [ReceivedDate] DATETIME2 NOT NULL, 
    [Amount] DECIMAL(18, 2) NOT NULL,
    CONSTRAINT [PK_Interest] PRIMARY KEY ([Id]),
    INDEX [IX_Interest] ([ReceivedDate])
)
