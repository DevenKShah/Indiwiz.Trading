CREATE TABLE [dbo].[Investment]
(
    [Id] BIGINT NOT NULL IDENTITY(1,1), 
    [InvestmentDate] DATETIME2 NOT NULL, 
    [Amount] DECIMAL NOT NULL,
    CONSTRAINT [PK_Investment] PRIMARY KEY ([Id]),
    INDEX [IX_Investment] ([InvestmentDate])
)
