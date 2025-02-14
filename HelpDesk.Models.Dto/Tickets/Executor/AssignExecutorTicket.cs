namespace HelpDesk.Models.Dto.Tickets.Executor;

public class AssignExecutorTicket 
{
    public long UserId { get; set; }
    public long TicketId { get; set; }
    public long AppointedWhoId { get; set; }
}