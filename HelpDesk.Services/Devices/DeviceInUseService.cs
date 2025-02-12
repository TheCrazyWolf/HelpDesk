using HelpDesk.Models.DLA.Devices;
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

    public async Task AttachDeviceToTicket(IEnumerable<string> devicesId, long ticketEntityId)
    {
        foreach (var id in devicesId)
        {
            var deviceInUser = new DeviceInUseTicket(id.Trim().ToLower(), ticketEntityId);
            await ef.AddAsync(deviceInUser);
        }

        await ef.SaveChangesAsync();
    }
}