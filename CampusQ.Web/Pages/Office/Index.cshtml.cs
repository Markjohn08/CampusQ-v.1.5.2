using CampusQ.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CampusQ.Web.Pages.Office
{
    public class IndexModel : PageModel
    {
        private readonly OfficeQueueService _officeQueueService;
        private readonly IConfiguration _configuration;

        public IndexModel(OfficeQueueService officeQueueService, IConfiguration configuration)
        {
            _officeQueueService = officeQueueService;
            _configuration = configuration;
        }

        public OfficeQueueResult Queue { get; set; } = new();

        public int RefreshIntervalSeconds { get; set; }

        public IReadOnlyList<string> KnownOffices => OfficeQueueService.KnownOffices;

        public void OnGet(string service)
        {
            RefreshIntervalSeconds = _configuration.GetValue<int?>("TicketMonitoring:RefreshIntervalSeconds") ?? 10;
            Queue = _officeQueueService.GetQueue(service);
        }
    }
}
