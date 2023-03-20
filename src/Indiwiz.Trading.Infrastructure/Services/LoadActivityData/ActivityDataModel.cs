using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indiwiz.Trading.Domain.Entities;

namespace Indiwiz.Trading.Infrastructure.Services.LoadActivityData
{
    public class ActivityDataModel
    {
        public string? Title { get; set; }
        public string? Type { get; set; }
        public DateTime? TimeStamp { get; set; }
        public decimal? TotalAmount { get; set; }
        public OrderType? BuySell {get; set;}
        public string? Ticker { get; set; }
        public string? OrderId { get; set; }
        public CurrencyName? InstrumentCurrency { get; set; }
        public decimal PricePerShare { get; set; }
    }
}