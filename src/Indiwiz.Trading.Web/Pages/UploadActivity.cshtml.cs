using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Infrastructure.Services.LoadActivityData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Indiwiz.Trading.Web.Pages
{
    public class UploadActivity : PageModel
    {
        private readonly ILogger<UploadActivity> _logger;
        private readonly ILoadActivityDataService _loadActivityDataService;

        public UploadActivity(ILogger<UploadActivity> logger, ILoadActivityDataService loadActivityDataService)
        {
            _logger = logger;
            _loadActivityDataService = loadActivityDataService;
        }

        [BindProperty]
        public IFormFile Upload { get; set; } = null!;
        public void OnPostAsync()
        {
            using var reader = Upload.OpenReadStream();
            var data = _loadActivityDataService.LoadData(reader);
            var instruments = data.Where(d => d.ActivityType == ActivityType.Order).DistinctBy(a => a.ISIN).Select(x => (Instrument)x).ToList();
            _logger.LogInformation("Total instruments found {count}", instruments.Count);
        }
    }
}