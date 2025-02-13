using HelpDesk.Models.Dto.Common;

namespace HelpDesk.Models.Dto.Tickets.Tickets;

public class TicketUpdateDeadLine : WithId
{
    public DateTime? Deadline { get; set; }
}