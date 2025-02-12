using HelpDesk.Models.Dto.Common;
using HelpDesk.Models.Enums.Tickets;

namespace HelpDesk.Models.Dto.Tickets.Tickets;

public class TicketUpdateType : WithId
{
    public TicketType Type { get; set; }
}