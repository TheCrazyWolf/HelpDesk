using HelpDesk.Modals.Devices;
using HelpDesk.Models.Dto.Devices;
using HelpDesk.Models.PLA.Devices;
using MudBlazor;

namespace HelpDesk.Modals;

public static class ModalExtensions
{
    public static async Task<DeviceView?> ShowDeviceDialog(this IDialogService dialogService, DeviceDto deviceDto, string title = "Device")
    {
        var parameters = new DialogParameters<DeviceDialog> { { "DeviceDto", deviceDto } };
        var dialog = await dialogService.ShowAsync<DeviceDialog>(title, parameters);
        await dialog.Result;
        if(dialog.Result.Result?.Data is null && dialog.Result.Result?.Data is not bool) return null;
        return (DeviceView)dialog.Result.Result.Data;
    }
}