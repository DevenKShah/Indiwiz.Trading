CREATE TABLE [dbo].[Instrument]
(
    [Id] BIGINT NOT NULL IDENTITY(1,1),
    [ISIN] VARCHAR(20) NOT NULL,
    [Title] NVARCHAR(200) NOT NULL,
    [CurrencyName] NVARCHAR(10) NOT NULL,
    [Ticker] NVARCHAR(10) NOT NULL
    CONSTRAINT [PK_Instrument] PRIMARY KEY ([Id]),
    CONSTRAINT [UK_Instrument] UNIQUE ([ISIN])
)
