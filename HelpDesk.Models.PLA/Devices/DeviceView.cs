using HelpDesk.Models.DLA.Devices;
using HelpDesk.Models.Enums.Devices;

namespace HelpDesk.Models.PLA.Devices;

public class DeviceView
{
    public string InvNumber { get; set; } = string.Empty;
    public DeviceType Type { get; set; }

    public DeviceView()
    {
        
    }
    
    public DeviceView(Device? xDevice)
    {
        if (xDevice?.InvNumber != null)
        {
            InvNumber = xDevice.InvNumber;
            Type = xDevice.Type;
        }
    }

}