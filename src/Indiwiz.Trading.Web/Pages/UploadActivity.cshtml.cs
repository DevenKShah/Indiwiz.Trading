using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Indiwiz.Trading.Infrastructure.Services.LoadActivityData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Indiwiz.Trading.Web.Pages
{
    public class UploadActivity : PageModel
    {
        private readonly ILogger<UploadActivity> _logger;
        private readonly ILoadActivityDataService _loadActivityDataService;
        private readonly ITradingDataContext _tradingDataContext;
        private readonly IInstrumentsRepository _instrumentsRepository;

        public UploadActivity(
            ILogger<UploadActivity> logger,
            ILoadActivityDataService loadActivityDataService,
            ITradingDataContext tradingDataContext,
            IInstrumentsRepository instrumentsRepository)
        {
            _logger = logger;
            _loadActivityDataService = loadActivityDataService;
            _tradingDataContext = tradingDataContext;
            _instrumentsRepository = instrumentsRepository;
        }

        [BindProperty]
        public IFormFile Upload { get; set; } = null!;

        public async Task OnPostAsync(CancellationToken cancellationToken)
        {
            List<ActivityDataModel> importedData = LoadDataFromCsv();

            await AddNewInstruments(importedData);

            await AddOrders(importedData);

            await _tradingDataContext.SaveChangesAsync(cancellationToken);
        }

        private List<ActivityDataModel> LoadDataFromCsv()
        {
            using var reader = Upload.OpenReadStream();
            return _loadActivityDataService.LoadData(reader);
        }

        private async Task AddNewInstruments(List<ActivityDataModel> data)
        {
            var instruments = data.Where(d => d.ActivityType == ActivityType.Order).DistinctBy(a => a.ISIN).Select(x => (Instrument)x);
            var existingInstruments = await _instrumentsRepository.GetInstruments();

            var newInstruments = instruments.ExceptBy(existingInstruments.Select(i => i.ISIN), i => i.ISIN);

            await _instrumentsRepository.AddInstruments(newInstruments);

            _logger.LogInformation("Total instruments added {count}", newInstruments.Count());
        }

        private async Task AddOrders(List<ActivityDataModel> data)
        {
            var instruments = await _instrumentsRepository.GetInstruments();

            var orders = data.Where(d => d.ActivityType == ActivityType.Order);
            foreach (var order in orders)
            {
                var instrument = instruments.Single(i => i.ISIN == order.ISIN);
                if (instrument.Orders.Any(o => o.OrderId == order.OrderId)) continue;
                instrument.Orders.Add(order);
            }
        }
    }
}