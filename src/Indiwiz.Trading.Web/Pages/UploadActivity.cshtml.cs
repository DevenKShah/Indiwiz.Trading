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
        private readonly IInterestRepository _interestRepository;
        private readonly IInvestmentsRepository _investmentsRepository;

        public UploadActivity(
            ILogger<UploadActivity> logger,
            ILoadActivityDataService loadActivityDataService,
            ITradingDataContext tradingDataContext,
            IInstrumentsRepository instrumentsRepository,
            IInterestRepository interestRepository,
            IInvestmentsRepository investmentsRepository)
        {
            _logger = logger;
            _loadActivityDataService = loadActivityDataService;
            _tradingDataContext = tradingDataContext;
            _instrumentsRepository = instrumentsRepository;
            _interestRepository = interestRepository;
            _investmentsRepository = investmentsRepository;
        }

        [BindProperty]
        public IFormFile Upload { get; set; } = null!;

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            List<ActivityDataModel> importedData = LoadDataFromCsv();

            await AddNewInstruments(importedData, cancellationToken);

            await AddOrders(importedData, cancellationToken);

            await AddInterestReceived(importedData, cancellationToken);

            await AddInvestments(importedData, cancellationToken);

            return new RedirectToPageResult("Index");
        }

        private List<ActivityDataModel> LoadDataFromCsv()
        {
            using var reader = Upload.OpenReadStream();
            return _loadActivityDataService.LoadData(reader);
        }

        private async Task AddNewInstruments(List<ActivityDataModel> data, CancellationToken cancellationToken)
        {
            var instruments = data.Where(d => d.ActivityType == ActivityType.Order).DistinctBy(a => a.ISIN).Select(x => (Instrument)x);
            var existingInstruments = await _instrumentsRepository.GetInstruments();

            var newInstruments = instruments.ExceptBy(existingInstruments.Select(i => i.ISIN), i => i.ISIN);

            await _instrumentsRepository.AddInstruments(newInstruments);

            await _tradingDataContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Total instruments added {count}", newInstruments.Count());
        }

        private async Task AddOrders(List<ActivityDataModel> data, CancellationToken cancellationToken)
        {
            var instruments = await _instrumentsRepository.GetInstruments();

            var orders = data.Where(d => d.ActivityType == ActivityType.Order);
            foreach (var order in orders)
            {
                var instrument = instruments.Single(i => i.ISIN == order.ISIN);
                if (instrument.Orders.Any(o => o.OrderId == order.OrderId)) continue;
                instrument.Orders.Add(order);
            }

            await _tradingDataContext.SaveChangesAsync(cancellationToken);
        }

        private async Task AddInterestReceived(List<ActivityDataModel> data, CancellationToken cancellationToken)
        {
            var existingData = await _interestRepository.GetAllInterests();

            var latestImportedDate = existingData.OrderByDescending(d => d.ReceivedDate).FirstOrDefault()?.ReceivedDate;

            var interestReceived = data.Where(d => d.ActivityType == ActivityType.InterestFromCash && d.TimeStamp > latestImportedDate.GetValueOrDefault()).Select(i => (Interest)i).ToList();

            await _interestRepository.AddInterests(interestReceived);

            await _tradingDataContext.SaveChangesAsync(cancellationToken);
        }

        private async Task AddInvestments(List<ActivityDataModel> data, CancellationToken cancellationToken)
        {
            var existingData = await _investmentsRepository.GetAllInvestments();

            var latestImportedDate = existingData.OrderByDescending(d => d.InvestmentDate).FirstOrDefault()?.InvestmentDate;

            var investmentsMade = data.Where(d => d.ActivityType == ActivityType.Topup && d.TimeStamp > latestImportedDate.GetValueOrDefault()).Select(i => (Investment)i).ToList();

            await _investmentsRepository.AddInvestments(investmentsMade);

            await _tradingDataContext.SaveChangesAsync(cancellationToken);
        }
    }
}