using CsvHelper.Configuration.Attributes;
using Indiwiz.Trading.Domain.Entities;

namespace Indiwiz.Trading.Infrastructure.Services.LoadActivityData
{
    public class ActivityDataModel
    {
        public string? Title { get; set; }
        public string? Type { get; set; }
        [Name("Timestamp")]
        public DateTime? TimeStamp { get; set; }
        [Name("Total Amount")]
        public decimal? TotalAmount { get; set; }
        [Name("Buy / Sell")]
        public OrderType? BuySell { get; set; }
        public string? Ticker { get; set; }
        public string? ISIN { get; set; }
        public decimal? Quantity { get; set; }
        [Name("Order ID")]
        public string? OrderId { get; set; }
        [Name("Instrument Currency")]
        public CurrencyName? InstrumentCurrency { get; set; }
        [Name("Price per Share")]
        public decimal? PricePerShare { get; set; }

        public static implicit operator Instrument(ActivityDataModel source) =>
            new Instrument
            {
                ISIN = source.ISIN!,
                Title = source.Title!,
                Currency = source.InstrumentCurrency.GetValueOrDefault(),
                Ticker = source.Ticker!,
            };
    }
}