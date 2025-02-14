using HelpDesk.Models.DLA.Devices;
using HelpDesk.Models.DLA.Identity;
using HelpDesk.Models.DLA.Tickets;
using HelpDesk.Models.Dto.Auth;
using HelpDesk.Models.Dto.Tickets.Executor;
using HelpDesk.Models.Dto.Tickets.History;
using HelpDesk.Models.Dto.Tickets.Tickets;
using HelpDesk.Models.Enums.Extensions;
using HelpDesk.Models.Enums.Tickets;
using HelpDesk.Models.PLA.Accounts;
using HelpDesk.Models.PLA.Devices;
using HelpDesk.Models.PLA.Tickets;
using HelpDesk.Services.Documents;
using HelpDesk.Storage;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services.Tickets;

public class TicketHistoryService(HelpDeskContext ef, DocumentService documentService, IMapper mapper)
{
    public async Task<IList<TicketHistoryView>> GetTicketHistoryAsync(long idTicket)
    {
        var items = await ef.TicketHistories
            .Include(x=> x.User)
            .Where(x=> x.TicketId == idTicket)
            .Select(x=> new TicketHistoryView
            {
                Id = x.Id,
                Message = x.Message,
                CreatedBy = mapper.Map<IdentityView>(x.User!),
                IsHideForUser = x.IsHideForUser,
                CreatedAt = x.CreatedAt
            }).OrderByDescending(x=> x.CreatedAt)
            .ToListAsync();

        foreach (var history in items) history.Files = await documentService.GetDocumentByHistoryTicket(history.Id);
        return items;
    }
    
    public async Task CreateTicketHistory(TicketHistoryNew ticketHistory, DeskToken? deskToken)
    {
        var ticketHistoryEntity = mapper.Map<TicketHistory>(ticketHistory);
        ticketHistoryEntity.CreatedAt = DateTime.Now;
        ticketHistoryEntity.UserId = deskToken.Id;
        await ef.AddAsync(ticketHistoryEntity);
        await ef.SaveChangesAsync();
        await documentService.AttachDocumentToTicketHistory(ticketHistoryEntity, ticketHistory.Files.Select(x=> x.Id).ToArray());
    }

    public async Task NewHistoryDeadLine(Ticket ticket, DeskToken deskToken)
    {
        var history = new TicketHistory
        {
            TicketId = ticket.Id,
            UserId = deskToken.Id,
            CreatedAt = DateTime.Now,
            Message = $"Назначил(а) новый срок решение заявки: {ticket.Deadline?.ToString("dd.MM.yyyy")}"
        };
        await ef.AddAsync(history);
        await ef.SaveChangesAsync();
    }

    public async Task NewHistoryStatus(TicketUpdateStatus ticketUpdateStatus, DeskToken deskToken)
    {
        var history = new TicketHistory
        {
            TicketId = ticketUpdateStatus.Id,
            UserId = deskToken.Id,
            CreatedAt = DateTime.Now,
            Message = $"Сменил(а) статус заявки на: {ticketUpdateStatus.Status.GetDisplayName()}"
        };
        await ef.AddAsync(history);
        await ef.SaveChangesAsync();
    }

    public async Task NewHistoryType(TicketUpdateType ticketUpdateType, DeskToken deskToken)
    {
        var history = new TicketHistory
        {
            TicketId = ticketUpdateType.Id,
            UserId = deskToken.Id,
            CreatedAt = DateTime.Now,
            Message = $"Изменил(а) категорию заявки на: {ticketUpdateType.Type.GetDisplayName()}"
        };
        await ef.AddAsync(history);
        await ef.SaveChangesAsync();
    }

    public async Task NewUpdatePriority(TicketUpdatePriority ticketUpdatePriority, DeskToken deskToken)
    {
        var history = new TicketHistory
        {
            TicketId = ticketUpdatePriority.Id,
            UserId = deskToken.Id,
            CreatedAt = DateTime.Now,
            Message = $"Установил(а) новый приоритет заявки на: {ticketUpdatePriority.Priority.GetDisplayName()}"
        };
        await ef.AddAsync(history);
        await ef.SaveChangesAsync();
    }

    public async Task NewHistoryAssignedOfTicket(AssignExecutorTicket assignExecutorTicket, Account? identity, DeskToken deskToken)
    {
        var history = new TicketHistory
        {
            TicketId = assignExecutorTicket.TicketId,
            UserId = deskToken.Id,
            CreatedAt = DateTime.Now,
            Message = $"Назначил(а) исполнителя по заявке: {identity?.LastName} {identity?.FirstName} {identity?.MiddleName}"
        };
        await ef.AddAsync(history);
        await ef.SaveChangesAsync();
    }


    public async Task NewHistoryUnAssignedOfTicket(TicketExecutor executor, Account? identity, DeskToken? deskToken)
    {
        var history = new TicketHistory
        {
            TicketId = executor.TicketId,
            UserId = deskToken?.Id,
            CreatedAt = DateTime.Now,
            Message = $"Снял(а) исполнителя по заявке: {identity?.LastName} {identity?.FirstName} {identity?.MiddleName}"
        };
        await ef.AddAsync(history);
        await ef.SaveChangesAsync();
    }

    public async Task NewHistoryAttachDevice(DeviceView deviceView, DeviceInUseTicket deviceInUser, DeskToken deskToken)
    {
        var history = new TicketHistory
        {
            TicketId = deviceInUser.TicketId,
            UserId = deskToken?.Id,
            CreatedAt = DateTime.Now,
            Message = $"Добавил(а) устройство в заявку: {deviceView.Type.GetDisplayName()} инв. № {deviceView.InvNumber}"
        };
        await ef.AddAsync(history);
        await ef.SaveChangesAsync();
    }

    public async Task NewHistoryUnAttachedDevice(DeviceInUseTicket deviceInUse, DeskToken deskToken)
    {
        var history = new TicketHistory
        {
            TicketId = deviceInUse.TicketId,
            UserId = deskToken?.Id,
            CreatedAt = DateTime.Now,
            Message = $"Удалил(а) устройство из заявки: {deviceInUse.Device?.Type.GetDisplayName()} инв. № {deviceInUse.Device?.InvNumber}"
        };
        await ef.AddAsync(history);
        await ef.SaveChangesAsync();
    }
}