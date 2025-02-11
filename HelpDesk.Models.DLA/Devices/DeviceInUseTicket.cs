using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models.DLA.Devices;

public class DeviceInUseTicket
{
    public string? DeviceId { get; set; }
    [ForeignKey("DeviceId")] public Device? Device { get; set; }
    public long? TicketId { get; set; }
    [ForeignKey("TicketId")] public Ticket? Ticket { get; set; }
}