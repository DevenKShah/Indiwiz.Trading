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
            using var reader = Upload.OpenReadStream();
            var data = _loadActivityDataService.LoadData(reader);
            var instruments = data.Where(d => d.ActivityType == ActivityType.Order).DistinctBy(a => a.ISIN).Select(x => (Instrument)x).ToList();
            await _instrumentsRepository.AddInstruments(instruments);
            await _tradingDataContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Total instruments found {count}", instruments.Count);
        }
    }
}