using HelpDesk.Models.DLA.Tickets;
using HelpDesk.Models.Dto.Auth;
using HelpDesk.Models.Dto.Tickets.Tickets;
using HelpDesk.Models.PLA.Tickets;
using HelpDesk.Services.Devices;
using HelpDesk.Services.Documents;
using HelpDesk.Services.ThrowHelpers;
using HelpDesk.Storage;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.

namespace HelpDesk.Services.Tickets;

public class TicketService(
    HelpDeskContext ef,
    DeviceInUseService deviceInUseService,
    TicketExecutorService ticketExecutorService,
    TicketHistoryService ticketHistoryService,
    DocumentService documentService,
    IMapper mapper)
{
    public async Task<TicketView?> GetTicket(long idTicket)
    {
        var ticket = await ef.Tickets.FirstOrDefaultAsync(t => t.Id == idTicket);
        ticket.ThrowIfNull(nameof(Ticket));
        var dto = mapper.Map<TicketView>(ticket);
        dto.Devices = await deviceInUseService.GetDevicesByTicket(idTicket);
        dto.Executors = await ticketExecutorService.GetExecutorsByTicket(idTicket);
        dto.Files = await documentService.GetDocumentByTicket(idTicket);
        dto.Chat = await ticketHistoryService.GetTicketHistoryAsync(idTicket);
        return dto;
    }

    public async Task<long> CreateTicket(TicketCreateDto ticket)
    {
        var ticketEntity = mapper.Map<Ticket>(ticket);
        ticketEntity.UpdatedAt = DateTime.Now;
        await ef.AddAsync(ticketEntity);
        await ef.SaveChangesAsync();
        await documentService.AttachDocumentToTicket(ticketEntity, ticket.Files.Select(x => x.Id).ToArray());
        await deviceInUseService.AttachDeviceToTicket(ticket.Devices, ticketEntity.Id);
        return ticketEntity.Id;
    }

    public async Task UpdateDeadLine(TicketUpdateDeadLine ticketUpdateDeadLine, DeskToken? deskToken)
    {
        var ticket = await ef.Tickets.FirstOrDefaultAsync(x => x.Id == ticketUpdateDeadLine.Id);
        ticket.ThrowIfNull(nameof(Ticket));
        mapper.Map(ticketUpdateDeadLine, ticket);
        ticket.UpdatedAt = DateTime.Now;
        ef.Update(ticket);
        await ef.SaveChangesAsync();
        await ticketHistoryService.NewHistoryDeadLine(ticket, deskToken);
    }

    public async Task UpdateStatus(TicketUpdateStatus ticketUpdateStatus, DeskToken? deskToken)
    {
        var ticket = await ef.Tickets.FirstOrDefaultAsync(x => x.Id == ticketUpdateStatus.Id);
        ticket.ThrowIfNull(nameof(Ticket));
        mapper.Map(ticketUpdateStatus, ticket);
        ticket.UpdatedAt = DateTime.Now;
        ef.Update(ticket);
        await ef.SaveChangesAsync();
        await ticketHistoryService.NewHistoryStatus(ticketUpdateStatus, deskToken);
    }

    public async Task UpdateType(TicketUpdateType ticketUpdateType, DeskToken? deskToken)
    {
        var ticket = await ef.Tickets.FirstOrDefaultAsync(x => x.Id == ticketUpdateType.Id);
        ticket.ThrowIfNull(nameof(Ticket));
        mapper.Map(ticketUpdateType, ticket);
        ticket.UpdatedAt = DateTime.Now;
        ef.Update(ticket);
        await ef.SaveChangesAsync();
        await ticketHistoryService.NewHistoryType(ticketUpdateType, deskToken);
    }

    public async Task UpdatePriority(TicketUpdatePriority ticketUpdatePriority, DeskToken? deskToken)
    {
        var ticket = await ef.Tickets.FirstOrDefaultAsync(x => x.Id == ticketUpdatePriority.Id);
        ticket.ThrowIfNull(nameof(Ticket));
        mapper.Map(ticketUpdatePriority, ticket);
        ticket.UpdatedAt = DateTime.Now;
        ef.Update(ticket);
        await ef.SaveChangesAsync();
        await ticketHistoryService.NewUpdatePriority(ticketUpdatePriority, deskToken);
    }

    public async Task<IEnumerable<TicketView>?> ShowTicketsByAccountId(long id)
    {
        var tickets = await ef.Tickets.Where(x => x.UserId == id)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return mapper.Map<IEnumerable<TicketView>>(tickets);
    }

#pragma warning disable CS8604 // Possible null reference argument.
    public async Task<TicketUpdateDeadLine> GetDtoUpdateDeadLine(long idTicket)
    {
        return mapper.Map<TicketUpdateDeadLine>(await ef.Tickets.FirstOrDefaultAsync(x => x.Id == idTicket));
    }

    public async Task<TicketUpdateStatus> GetDtoUpdateStatud(long idTicket)
    {
        return mapper.Map<TicketUpdateStatus>(await ef.Tickets.FirstOrDefaultAsync(x => x.Id == idTicket));
    }

    public async Task<TicketUpdateType> GetDtoUpdateType(long idTicket)
    {
        return mapper.Map<TicketUpdateType>(await ef.Tickets.FirstOrDefaultAsync(x => x.Id == idTicket));
    }


    public async Task<TicketUpdatePriority> GetDtoUpdatePriority(long idTicket)
    {
        return mapper.Map<TicketUpdatePriority>(await ef.Tickets.FirstOrDefaultAsync(x => x.Id == idTicket));
    }
#pragma warning restore CS8604 // Possible null reference argument.
}