using HelpDesk.Models.Enums.Devices;

namespace HelpDesk.Models.Dto.Devices;

public class DeviceDto
{
    public string InvNumber { get; set; } = string.Empty;
    public DeviceType Type { get; set; }
}