using System.Globalization;
using CsvHelper;

namespace Indiwiz.Trading.Infrastructure.Services.LoadActivityData;

public interface ILoadActivityDataService
{
    public Task<IEnumerable<ActivityDataModel>> LoadData(Stream stream);
}

public class LoadActivityDataService : ILoadActivityDataService
{
    public async Task<IEnumerable<ActivityDataModel>> LoadData(Stream stream)
    {
        using (StreamReader reader = new(stream))
        using (CsvReader csv = new(reader, CultureInfo.InvariantCulture))
        {
            return csv.GetRecords<ActivityDataModel>();
        }
    }
}