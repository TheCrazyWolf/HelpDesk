using HelpDesk.Models.Dto.Common;

namespace HelpDesk.Models.Dto.Tickets.Executor;

public class AssignExecutorTicket : WithId
{
    public long TicketId { get; set; }
}