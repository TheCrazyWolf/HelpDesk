using HelpDesk.Modals.Devices;
using HelpDesk.Modals.Tickets;
using HelpDesk.Models.Dto.Devices;
using HelpDesk.Models.Dto.Tickets.Tickets;
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
    
    public static async Task ShowUpdateDeadline(this IDialogService dialogService, long ticketId, string title = "Редактирования срока решения")
    {
        var parameters = new DialogParameters<TicketEditDeadline> { { "IdTicket", ticketId } };
        var dialog = await dialogService.ShowAsync<TicketEditDeadline>(title, parameters);
        await dialog.Result;
    }
    
    public static async Task ShowUpdateStatus(this IDialogService dialogService, long ticketId, string title = "Новый статус заявки")
    {
        var parameters = new DialogParameters<TicketUpdateStatus> { { "IdTicket", ticketId } };
        var dialog = await dialogService.ShowAsync<TicketEditStatus>(title, parameters);
        await dialog.Result;
    }
}