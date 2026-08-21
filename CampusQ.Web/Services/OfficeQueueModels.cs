namespace CampusQ.Web.Services
{
    public class OfficeQueueTicket
    {
        public int TicketNumber { get; set; }
        public string TicketLabel { get; set; } = "";
        public string Purpose { get; set; } = "";
        public int Position { get; set; }
        public DateTime TimeAdded { get; set; }
    }

    public class OfficeQueueResult
    {
        public string Service { get; set; } = "";
        public bool ServiceRecognized { get; set; }
        public List<OfficeQueueTicket> Tickets { get; set; } = new();
        public int TotalWaiting => Tickets.Count;
    }
}
