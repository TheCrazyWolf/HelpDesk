using HelpDesk.Models.Enums.Tickets;

namespace HelpDesk.Models.Dto.Tickets.Tickets;

public class TicketCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketType Type { get; set; }
    public TicketLevelPriority Priority { get; set; }
    public IList<long> DocumentsIds { get; set; } = new List<long>();
}