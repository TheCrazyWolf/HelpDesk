﻿using HelpDesk.Models.DLA.Devices;
using HelpDesk.Models.Dto.Devices;
using HelpDesk.Models.PLA.Devices;
using HelpDesk.Services.ThrowHelpers;
using HelpDesk.Storage;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.

namespace HelpDesk.Services.Devices;

public class DeviceService(HelpDeskContext ef, DeviceInUseService deviceInUseService, IMapper mapper)
{
    public async Task<Device?> GetDeviceByIdAsync(string deviceId)
    {
        return await ef.Devices.FirstOrDefaultAsync(x => x.InvNumber == deviceId.Trim().ToUpper());
    }

    public async Task<Device> AddDeviceAsync(DeviceDto device)
    {
        var newDevice = mapper.Map<Device>(device);
        newDevice.InvNumber = device.InvNumber.Trim().ToUpper();
        ef.Add(newDevice);
        await ef.SaveChangesAsync();
        return newDevice;
    }

    public async Task<Device> UpdateDeviceAsync(DeviceDto device)
    {
        var curDevice = await GetDeviceByIdAsync(device.InvNumber);
        if (curDevice is null) return await AddDeviceAsync(device);
        mapper.Map(device, curDevice);
        curDevice.InvNumber = device.InvNumber.Trim().ToUpper();
        ef.Update(curDevice);
        await ef.SaveChangesAsync();
        return curDevice;
    }

    public async Task<DeviceView> CreateOrUpdate(DeviceDto device)
    {
        return mapper.Map<DeviceView>(await UpdateDeviceAsync(device));
    }

    public async Task RemoveDevice(string invNumber)
    {
        var device = await GetDeviceByIdAsync(invNumber);
        device.ThrowIfNull(nameof(Device));
        if(await deviceInUseService.IsDeviceInUse(invNumber)) throw new Exception("Устройство нельзя удалить, оно используется в заявках");
        ef.Remove(device);
        await ef.SaveChangesAsync();
    }
}