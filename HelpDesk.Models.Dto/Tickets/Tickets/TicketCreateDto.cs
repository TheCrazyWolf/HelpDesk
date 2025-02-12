using HelpDesk.Models.Enums.Tickets;
using HelpDesk.Models.PLA.Devices;
using HelpDesk.Models.PLA.Documents;

namespace HelpDesk.Models.Dto.Tickets.Tickets;

public class TicketCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketType Type { get; set; }
    public TicketLevelPriority Priority { get; set; }
    public string PlaceOfIssue { get; set; } = string.Empty;
    public IList<DeskDocumentView> Files { get; set; } = new List<DeskDocumentView>();
    public IList<DeviceView> Devices { get; set; } = new List<DeviceView>();
    public long UserId { get; set; }
}