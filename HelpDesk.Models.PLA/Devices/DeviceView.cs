using HelpDesk.Models.Enums.Devices;

namespace HelpDesk.Models.PLA.Devices;

public class DeviceView
{
    public string InvNumber { get; set; } = string.Empty;
    public DeviceType Type { get; set; }
}