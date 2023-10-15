using Flurl;
using Newtonsoft.Json.Linq;

namespace Indiwiz.Trading.Infrastructure.Services.LoadActivityData;

public interface IStockReaderService
{
    Task<decimal> GetLastClosingPrice(string ticker, string? exchange);
}

internal class AlphaVantageService : IStockReaderService
{
    const string BaseUrl = "https://www.alphavantage.co/query/";
    const string DailyPriceLookupFunctionName = "TIME_SERIES_DAILY";
    const string OutputSizeFull = "full";
    const string AlphaVantageApiKey = "HTEVQ9QW99QF57W2";
    private readonly HttpClient _httpClient;

    public AlphaVantageService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<decimal> GetLastClosingPrice(string ticker, string? exchange)
    {
        string symbol = string.IsNullOrEmpty(exchange) ? ticker : $"{ticker}.{exchange}";
        string url = GetTickerUrl(symbol);
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            JObject data = JObject.Parse(json);

            return data["Time Series (Daily)"]!.First().First()["4. close"]!.Value<decimal>();
        }
        Console.WriteLine("Error: Unable to retrieve data.");
        return 0;
    }

    public string GetTickerUrl(string symbol) =>
        BaseUrl.SetQueryParams(new
        {
            apikey = AlphaVantageApiKey,
            function = DailyPriceLookupFunctionName,
            symbol = symbol,
            outputsize = OutputSizeFull
        });
}
