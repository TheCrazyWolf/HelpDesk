using System.ComponentModel.DataAnnotations.Schema;
using HelpDesk.Models.DLA.Common;
using HelpDesk.Models.DLA.Identity;
using HelpDesk.Models.DLA.Tickets;

namespace HelpDesk.Models.DLA.Documents;

public class DeskDocument : DlaEntity
{
    public string FileName { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; } = DateTime.Now;
    public long? UserId { get; set; }
    [ForeignKey("UserId")] public Account? User { get; set; }
    
    public long? TicketId { get; set; }
    [ForeignKey("TicketId")] public Ticket? Ticket { get; set; }
    
    public long? TicketHistoryId { get; set; }
    [ForeignKey("TicketHistoryId")] public TicketHistory? TicketHistory { get; set; }
}