using HelpDesk.Models.Dto.Common;
using HelpDesk.Models.Enums.Tickets;

namespace HelpDesk.Models.Dto.Tickets;

public class TicketUpdateDeadLine : WithId
{
    public DateTime UpdatedAt { get; set; }
}