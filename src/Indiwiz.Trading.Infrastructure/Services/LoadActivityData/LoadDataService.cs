using Indiwiz.Trading.Domain.Interfaces;

namespace Indiwiz.Trading.Infrastructure.Services.LoadActivityData;

public class LoadActivityDataService : ILoadActivityDataService
{
    public Task LoadData()
    {
        return Task.CompletedTask;
    }
}