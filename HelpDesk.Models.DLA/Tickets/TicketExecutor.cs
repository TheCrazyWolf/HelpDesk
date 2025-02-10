using System.ComponentModel.DataAnnotations.Schema;
using HelpDesk.Models.DLA.Common;
using HelpDesk.Models.DLA.Identity;

namespace HelpDesk.Models.DLA.Tickets;

public class TicketExecutor : DlaEntity
{
    public long? UserId { get; set; }
    [ForeignKey("UserId")] public Account? User { get; set; }

    public long? TicketId { get; set; }
    [ForeignKey("TicketId")] public Ticket? Ticket { get; set; } = new Ticket();

    public DateTime AppointedAt { get; set; }

    public long? AppointedWhoId { get; set; }
    [ForeignKey("AppointedWhoId")] public Account? AppointedWho { get; set; }
}