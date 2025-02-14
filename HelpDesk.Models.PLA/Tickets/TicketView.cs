using HelpDesk.Models.Enums.Tickets;
using HelpDesk.Models.PLA.Common;
using HelpDesk.Models.PLA.Documents;

namespace HelpDesk.Models.PLA.Tickets;

public class TicketView : PlaEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PlaceOfIssue { get; set; } = string.Empty;
    public TicketType Type { get; set; }
    public TicketStatus Status { get; set; }
    public TicketLevelPriority Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? UserId { get; set; }
    public IList<TicketExecutorView> Executors { get; set; } = new List<TicketExecutorView>();
    public IList<TicketDeviceInUseView> Devices { get; set; } = new List<TicketDeviceInUseView>();
    public IList<DeskDocumentView> Files { get; set; } = new List<DeskDocumentView>();
}