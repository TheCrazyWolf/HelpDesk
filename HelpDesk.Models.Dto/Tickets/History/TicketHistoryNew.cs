using HelpDesk.Models.PLA.Documents;

namespace HelpDesk.Models.Dto.Tickets.History;

public class TicketHistoryNew
{
    public long TicketId { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsHideForUser { get; set; }
    public IList<DeskDocumentView> Files { get; set; } = new List<DeskDocumentView>();

    public TicketHistoryNew()
    {

    }

    public TicketHistoryNew(long ticketId, string message, bool isHideForUser, IList<DeskDocumentView> documents)
    {
        TicketId = ticketId;
        Message = message;
        IsHideForUser = isHideForUser;
        Files = documents;
    }
    
    public TicketHistoryNew(long? ticketId, long? userId, string message, bool isHideForUser)
    {
        TicketId = ticketId ?? throw new NullReferenceException(nameof(ticketId));
        Message = message;
        IsHideForUser = isHideForUser;
    }

}