using System.ComponentModel.DataAnnotations.Schema;
using HelpDesk.Models.DLA.Common;
using HelpDesk.Models.DLA.Identity;

namespace HelpDesk.Models.DLA.Tickets;

public class TicketHistory : DlaEntity
{
    public long? UserId { get; set; }
    [ForeignKey("UserId")] public Account? User { get; set; }
    public long? TicketId { get; set; }
    [ForeignKey("TicketId")] public Ticket? Ticket { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Today;
    public bool IsHideForUser { get; set; }

    public TicketHistory()
    {
        
    }

    public TicketHistory(long userId, string? message, bool isHideForUser)
    {
        UserId = userId;
        Message = message ?? string.Empty;
        CreatedAt = DateTime.Now;
        IsHideForUser = isHideForUser;
    }
}