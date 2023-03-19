using Indiwiz.Trading.Domain.Interfaces;

namespace Indiwiz.Trading.Application.Services;

public class LoadDataService : ILoadDataService
{
    public Task LoadData()
    {
        return Task.CompletedTask;
    }
}