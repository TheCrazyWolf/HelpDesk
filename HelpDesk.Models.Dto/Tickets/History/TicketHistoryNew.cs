namespace HelpDesk.Models.Dto.Tickets.History;

public class TicketHistoryNew
{
    public long TicketId { get; set; }
    public long UserId { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsHideForUser { get; set; }
    public IList<long> DocumentsIds { get; set; } = new List<long>();

    public TicketHistoryNew()
    {

    }

    public TicketHistoryNew(long ticketId, long userId, string message, bool isHideForUser, IList<long> documentsIds)
    {
        TicketId = ticketId;
        UserId = userId;
        Message = message;
        IsHideForUser = isHideForUser;
        DocumentsIds = documentsIds;
    }
    
    public TicketHistoryNew(long? ticketId, long? userId, string message, bool isHideForUser)
    {
        TicketId = ticketId ?? throw new NullReferenceException(nameof(ticketId));
        UserId = userId ?? throw new NullReferenceException(nameof(userId));
        Message = message;
        IsHideForUser = isHideForUser;
    }

}