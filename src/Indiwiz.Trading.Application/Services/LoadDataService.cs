using System.Threading.Tasks;
namespace Indiwiz.Trading.Application.Services;

public interface ILoadDataService
{
    public Task LoadData();
}


public class LoadDataService : ILoadDataService
{
    public Task LoadData()
    {
        return Task.CompletedTask;
    }
}