using HelpDesk.Models.Dto.Common;
using HelpDesk.Models.Enums.Tickets;

namespace HelpDesk.Models.Dto.Tickets.Tickets;

public class TicketUpdatePriority : WithId
{
    public TicketLevelPriority Priority { get; set; }
}