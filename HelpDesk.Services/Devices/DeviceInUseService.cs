using HelpDesk.Models.DLA.Devices;
using HelpDesk.Models.Dto.Auth;
using HelpDesk.Models.PLA.Devices;
using HelpDesk.Models.PLA.Tickets;
using HelpDesk.Services.ThrowHelpers;
using HelpDesk.Services.Tickets;
using HelpDesk.Storage;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services.Devices;

public class DeviceInUseService(HelpDeskContext ef, TicketHistoryService historyService, IMapper mapper)
{
    public async Task<bool> IsDeviceInUse(string deviceId)
    {
        return await ef.DeviceInUseTickets.AnyAsync(x => x.DeviceId == deviceId);
    }

    public async Task<IList<TicketDeviceInUseView>> GetDevicesByTicket(long idTicket)
    {
        return await ef.DeviceInUseTickets.Include(x => x.Device)
            .Where(x => x.TicketId == idTicket)
            .Select(x => new TicketDeviceInUseView()
            {
                Id = x.Id,
                Device = new DeviceView(x.Device)
            })
            .ToListAsync();
    }

    public async Task RemoveDeviceFromTicket(long deviceInUserId, DeskToken? deskToken)
    {
        deskToken.ThrowIfNull(nameof(deskToken));
        var deviceInUse = await ef.DeviceInUseTickets
            .Include(x=> x.Device)
            .FirstOrDefaultAsync(x => x.Id == deviceInUserId);
        deviceInUse.ThrowIfNull(nameof(DeviceInUseTicket));
        ef.Remove(deviceInUse);
        await ef.SaveChangesAsync();
        await historyService.NewHistoryUnAttachedDevice(deviceInUse, deskToken);
    }

    public async Task AttachDeviceToTicket(IList<DeviceView> ticketDevices, long ticketEntityId, DeskToken? deskToken)
    {
        deskToken.ThrowIfNull(nameof(DeskToken));
        foreach (var deviceView in ticketDevices)
        {
            var deviceInUser = new DeviceInUseTicket(deviceView.InvNumber.Trim().ToUpper(), ticketEntityId);
            await ef.AddAsync(deviceInUser);
            await ef.SaveChangesAsync();
            await historyService.NewHistoryAttachDevice(deviceView, deviceInUser, deskToken);
        }
    }
    
    
    public async Task AttachDeviceToTicket(IList<DeviceView> ticketDevices, long ticketEntityId)
    {
        foreach (var deviceView in ticketDevices)
        {
            var deviceInUser = new DeviceInUseTicket(deviceView.InvNumber.Trim().ToUpper(), ticketEntityId);
            await ef.AddAsync(deviceInUser);
            await ef.SaveChangesAsync();
        }
    }
}