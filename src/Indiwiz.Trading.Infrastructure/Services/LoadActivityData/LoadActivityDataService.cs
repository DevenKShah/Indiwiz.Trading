using System.Globalization;
using CsvHelper;

namespace Indiwiz.Trading.Infrastructure.Services.LoadActivityData;

public interface ILoadActivityDataService
{
    public List<ActivityDataModel> LoadData(Stream stream);
}

public class LoadActivityDataService : ILoadActivityDataService
{
    public List<ActivityDataModel> LoadData(Stream stream)
    {
        using StreamReader reader = new(stream);
        using CsvReader csv = new(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<ActivityDataModelMapper>();
        var records = csv.GetRecords<ActivityDataModel>().ToList();
        return records;
    }
}