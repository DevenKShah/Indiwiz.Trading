using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Indiwiz.Trading.Domain.Entities;

namespace Indiwiz.Trading.Infrastructure.Services.LoadActivityData;

public class ActivityDataModel
{
    public string? Title { get; set; }
    public ActivityType ActivityType { get; set; }
    public DateTime TimeStamp { get; set; }
    public decimal? TotalAmount { get; set; }
    public OrderType? BuySell { get; set; }
    public string? Ticker { get; set; }
    public string? ISIN { get; set; }
    public decimal? Quantity { get; set; }
    public string? OrderId { get; set; }
    public CurrencyName? InstrumentCurrency { get; set; }
    public decimal? PricePerShare { get; set; }

    public static implicit operator Instrument(ActivityDataModel source) =>
        new()
        {
            ISIN = source.ISIN!,
            Title = source.Title!,
            CurrencyName = source.InstrumentCurrency.GetValueOrDefault(),
            Ticker = source.Ticker!,
        };
}

public class ActivityDataModelMapper : ClassMap<ActivityDataModel>
{
    public static string? ReplaceUnderscores(string? value) => value?.Replace("_", string.Empty);

    public ActivityDataModelMapper()
    {
        Map(m => m.Title).Name("Title");
        Map(m => m.ActivityType)
            .Name("Type")
            .TypeConverterOption.NullValues(string.Empty)
            .TypeConverter(new EnumConverter<ActivityType>(ReplaceUnderscores));
        Map(m => m.TimeStamp).Name("Timestamp");
        Map(m => m.TotalAmount).Name("Total Amount");
        Map(m => m.BuySell)
            .Name("Buy / Sell")
            .TypeConverterOption.NullValues(string.Empty)
            .TypeConverter(new EnumConverter<OrderType>());
        Map(m => m.Ticker).Name("Ticker");
        Map(m => m.ISIN).Name("ISIN");
        Map(m => m.Quantity).Name("Quantity");
        Map(m => m.OrderId).Name("Order ID");
        Map(m => m.InstrumentCurrency).Name("Instrument Currency");
        Map(m => m.PricePerShare).Name("Price per Share");
    }
}

public class EnumConverter<T> : TypeConverter where T : Enum
{
    private readonly Func<string?, string?> _textConverter = (s) => s;

    public EnumConverter(Func<string?, string?> textConverter)
    {
        _textConverter = textConverter;
    }

    public EnumConverter() { }

    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        var normalisedText = _textConverter(text);
        Enum.TryParse(typeof(T), normalisedText, true, out var value);
        return value;
    }
}
