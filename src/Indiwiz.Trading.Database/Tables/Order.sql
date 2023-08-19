CREATE TABLE [dbo].[Order]
(
    [Id] BIGINT NOT NULL IDENTITY(1,1),
    [InstrumentId] BIGINT NOT NULL,
    [OrderId] NVARCHAR(20) NOT NULL,
    [TransactionType] NVARCHAR(10) NOT NULL,
    [OrderType] NVARCHAR(20) NOT NULL,
    [Quantity] DECIMAL(18, 5) NOT NULL,
    [RateInInstrumentCurrency] DECIMAL(18, 5) NOT NULL,
    [AmountInAccountCurrency] DECIMAL(18, 2) NOT NULL,
    [OrderDate] DATETIME2 NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY ([Id]),
    CONSTRAINT [UK_Order] UNIQUE ([OrderId]),
    CONSTRAINT [FK_Order_Instrument] FOREIGN KEY ([InstrumentId]) REFERENCES [Instrument]([Id]),
    INDEX [IX_Order_InstrumentId] ([InstrumentId]),
    INDEX [IX_Order_OrderDate] ([OrderDate])
)
