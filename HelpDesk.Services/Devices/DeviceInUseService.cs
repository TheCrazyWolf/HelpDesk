using HelpDesk.Models.DLA.Devices;
using HelpDesk.Models.PLA.Devices;
using HelpDesk.Models.PLA.Tickets;
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
            var deviceInUser = new DeviceInUseTicket(id.Trim().ToUpper(), ticketEntityId);
            await ef.AddAsync(deviceInUser);
        }

        await ef.SaveChangesAsync();
    }

    public async Task<IList<TicketDeviceInUseView>> GetDevicesByTicket(long idTicket)
    {
        return await ef.DeviceInUseTickets.Include(x => x.Device)
            .Where(x => x.TicketId == idTicket)
            .Select(x=> new TicketDeviceInUseView()
            {
                Id = x.Id,
                Device = new DeviceView(x.Device)
            })
            .ToListAsync();
    }

    public async Task RemoveDeviceFromTicket(long deviceInUserId)
    {
        var deviceInUse = await ef.DeviceInUseTickets.FirstOrDefaultAsync(x=> x.Id == deviceInUserId);
        if(deviceInUse == null) throw new Exception("Device not found");
        ef.Remove(deviceInUse);
        await ef.SaveChangesAsync();
    }
}