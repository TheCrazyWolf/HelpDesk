using HelpDesk.Models.Dto.Common;
using HelpDesk.Models.Enums.Tickets;

namespace HelpDesk.Models.Dto.Tickets;

public class TicketUpdateDetails : WithId
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketType Type { get; set; }
}