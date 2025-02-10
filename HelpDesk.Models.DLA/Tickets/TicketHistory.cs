using HelpDesk.Models.DLA.Common;

namespace HelpDesk.Models.DLA.Tickets;

public class TicketHistory : DlaEntity
{
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}