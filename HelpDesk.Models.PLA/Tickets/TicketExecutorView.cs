using HelpDesk.Models.PLA.Accounts;
using HelpDesk.Models.PLA.Common;

namespace HelpDesk.Models.PLA.Tickets;

public class TicketExecutorView: PlaEntity
{
    public IdentityView? Identity { get; set; }
}