using HelpDesk.Models.Dto.Common;
using HelpDesk.Models.Enums.Tickets;

namespace HelpDesk.Models.Dto.Tickets;

public class TicketUpdateStatus : WithId
{
    public TicketStatus Status { get; set; }
}