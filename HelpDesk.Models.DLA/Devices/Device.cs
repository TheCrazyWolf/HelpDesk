using System.ComponentModel.DataAnnotations;
using HelpDesk.Models.Enums.Devices;

namespace HelpDesk.Models.DLA.Devices;

public class Device
{
    [Key] public string InvNumber { get; set; } = string.Empty;
    public DeviceType Type { get; set; }
}