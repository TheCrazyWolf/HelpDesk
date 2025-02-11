using HelpDesk.Models.Enums.Tickets;
using HelpDesk.Models.PLA.Accounts;
using HelpDesk.Models.PLA.Common;

namespace HelpDesk.Models.PLA.Tickets;

public class TicketView : PlaEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketType Type { get; set; }
    public TicketStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public IdentityView? CreatedBy { get; set; }
    public IList<IdentityView>? Participants { get; set; } = new List<IdentityView>();
}