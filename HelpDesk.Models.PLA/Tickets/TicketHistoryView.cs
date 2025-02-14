using HelpDesk.Models.PLA.Accounts;
using HelpDesk.Models.PLA.Common;
using HelpDesk.Models.PLA.Documents;

namespace HelpDesk.Models.PLA.Tickets;

public class TicketHistoryView : PlaEntity
{
    public IdentityView? CreatedBy { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsHideForUser { get; set; }
    public IList<DeskDocumentView> Files { get; set; } = new List<DeskDocumentView>();
}