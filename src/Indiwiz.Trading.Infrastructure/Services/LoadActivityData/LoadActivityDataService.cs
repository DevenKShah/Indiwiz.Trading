using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Indiwiz.Trading.Infrastructure.Services.LoadActivityData;

public interface ILoadActivityDataService
{
    public List<ActivityDataModel> LoadData(Stream stream);
}

public class LoadActivityDataService : ILoadActivityDataService
{
    public List<ActivityDataModel> LoadData(Stream stream)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header
        };
        using (StreamReader reader = new(stream))
        using (CsvReader csv = new(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<ActivityDataModel>().ToList();
            return records;
        }
    }
}