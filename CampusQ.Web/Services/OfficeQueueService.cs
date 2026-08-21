using CampusQ.MVP.Data;

namespace CampusQ.Web.Services
{
    public class OfficeQueueService
    {
        /// <summary>
        /// Offices/services that can currently be selected on the QR Code page and viewed online.
        /// Must match the Service values written by the CampusQ desktop app (Form1.cs, Admission/Cashier/ServiceWindow).
        /// </summary>
        public static readonly string[] KnownOffices = { "Admission", "Cashier", "Registrar" };

        private readonly QueueRepository _queueRepository;

        public OfficeQueueService(QueueRepository queueRepository)
        {
            _queueRepository = queueRepository;
        }

        public OfficeQueueResult GetQueue(string service)
        {
            bool recognized = KnownOffices.Any(o => string.Equals(o, service, StringComparison.OrdinalIgnoreCase));
            var entries = _queueRepository.GetAllByService(service);

            var tickets = entries
                .Select((entry, index) => new OfficeQueueTicket
                {
                    TicketNumber = entry.TicketNumber,
                    TicketLabel = entry.TicketLabel,
                    Purpose = entry.Purpose,
                    Position = index + 1,
                    TimeAdded = entry.TimeAdded
                })
                .ToList();

            return new OfficeQueueResult
            {
                Service = service,
                ServiceRecognized = recognized,
                Tickets = tickets
            };
        }
    }
}
