using HelpDesk.Models.PLA.Documents;

namespace HelpDesk.Models.Dto.Tickets.History;

public class TicketHistoryCreate
{
    public long UserId { get; set; }
    public long TicketId { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsHideForUser { get; set; }
    public IList<DeskDocumentView> Files { get; set; } = new List<DeskDocumentView>();
}