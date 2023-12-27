
declare db_cursor CURSOR FOR
select InstrumentId from (
select 
    InstrumentId,
    case 
        when TransactionType = 'Sell' then -ABS(Quantity)
        else Quantity
    end as StockInHand,
    case 
        when TransactionType = 'Buy' then -ABS(AmountInAccountCurrency)
        else AmountInAccountCurrency
    end as Amount
from [Order] ) as O
inner join Instrument i on i.Id = o.InstrumentId
GROUP by InstrumentId, Title
HAVING sum(StockInHand) = 0

declare @instrumentId BIGINT

OPEN db_cursor
fetch next from db_cursor into @instrumentId

while @@FETCH_STATUS = 0
BEGIN
    update [Order]
    SET GroupId = NEWID()
    WHERE InstrumentId = @instrumentId

    fetch next from db_cursor into @instrumentId
END

close db_cursor
deallocate db_cursor

