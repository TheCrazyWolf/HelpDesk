using HelpDesk.Models.PLA.Common;
using HelpDesk.Models.PLA.Devices;

namespace HelpDesk.Models.PLA.Tickets;

public class TicketDeviceInUseView: PlaEntity
{
    public DeviceView? Device { get; set; }
}