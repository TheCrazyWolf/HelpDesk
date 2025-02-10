using HelpDesk.Models.PLA.Account;
using HelpDesk.Models.PLA.Common;

namespace HelpDesk.Models.PLA.Tickets;

public class TicketHistoryView : DlaEntity
{
    public IdentityView? CreatedBy { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}