using System.ComponentModel.DataAnnotations.Schema;
using HelpDesk.Models.DLA.Common;
using HelpDesk.Models.DLA.Tickets;

namespace HelpDesk.Models.DLA.Devices;

public class DeviceInUseTicket : DlaEntity
{
    public string? DeviceId { get; set; }
    [ForeignKey("DeviceId")] public Device? Device { get; set; }
    public long? TicketId { get; set; }
    [ForeignKey("TicketId")] public Ticket? Ticket { get; set; }
}