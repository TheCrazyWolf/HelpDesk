using HelpDesk.Storage;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services.Devices;

public class DeviceInUseService(HelpDeskContext ef, IMapper mapper)
{
    public async Task<bool> IsDeviceInUse(string deviceId)
    {
        return await ef.DeviceInUseTickets.AnyAsync(x=> x.DeviceId == deviceId);
    }
}