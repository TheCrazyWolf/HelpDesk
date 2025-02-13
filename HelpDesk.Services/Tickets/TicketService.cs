using HelpDesk.Models.DLA.Tickets;
using HelpDesk.Models.Dto.Tickets.Tickets;
using HelpDesk.Models.PLA.Tickets;
using HelpDesk.Services.Devices;
using HelpDesk.Services.Documents;
using HelpDesk.Storage;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services.Tickets;

public class TicketService(HelpDeskContext ef, DeviceInUseService deviceInUseService, DocumentService documentService, IMapper mapper)
{

    public async Task<TicketView?> GetTicket(long idTicket)
    {
        var ticket = await ef.Tickets.FirstOrDefaultAsync(t => t.Id == idTicket);
        if (ticket == null) throw new Exception("Заявка не найдена");
        return mapper.Map<TicketView>(ticket);
    }
    
    public async Task<long> CreateTicket(TicketCreateDto ticket)
    {
        var ticketEntity = mapper.Map<Ticket>(ticket);
        await ef.AddAsync(ticketEntity);
        await ef.SaveChangesAsync();
        await documentService.AttachDocumentToTicket(ticketEntity, ticket.Files.Select(x=> x.Id).ToArray());
        await deviceInUseService.AttachDeviceToTicket(ticket.Devices.Select(x => x.InvNumber), ticketEntity.Id);
        return ticketEntity.Id;
    }

    public async Task UpdateDeadLine(TicketUpdateDeadLine ticketUpdateDeadLine)
    {
        var ticket = await ef.Tickets.FirstOrDefaultAsync(x=> x.Id == ticketUpdateDeadLine.Id);
        if (ticket == null) throw new Exception("Заявка не найдена");
        mapper.Map(ticketUpdateDeadLine, ticket);
        ticket.UpdatedAt = DateTime.Now;
        ef.Update(ticket);
        await ef.SaveChangesAsync();
    }
    
    public async Task UpdateStatus(TicketUpdateStatus ticketUpdateStatus)
    {
        var ticket = await ef.Tickets.FirstOrDefaultAsync(x=> x.Id == ticketUpdateStatus.Id);
        if (ticket == null) throw new Exception("Заявка не найдена");
        mapper.Map(ticketUpdateStatus, ticket);
        ticket.UpdatedAt = DateTime.Now;
        ef.Update(ticket);
        await ef.SaveChangesAsync();
    }
    
    public async Task UpdateType(TicketUpdateType ticketUpdateType)
    {
        var ticket = await ef.Tickets.FirstOrDefaultAsync(x=> x.Id == ticketUpdateType.Id);
        if (ticket == null) throw new Exception("Заявка не найдена");
        mapper.Map(ticketUpdateType, ticket);
        ticket.UpdatedAt = DateTime.Now;
        ef.Update(ticket);
        await ef.SaveChangesAsync();
    }
    
    public async Task UpdatePriority(TicketUpdatePriority ticketUpdatePriority)
    {
        var ticket = await ef.Tickets.FirstOrDefaultAsync(x=> x.Id == ticketUpdatePriority.Id);
        if (ticket == null) throw new Exception("Заявка не найдена");
        mapper.Map(ticketUpdatePriority, ticket);
        ticket.UpdatedAt = DateTime.Now;
        ef.Update(ticket);
        await ef.SaveChangesAsync();
    }

    public async Task<IEnumerable<TicketView>?> ShowTicketsByAccountId(long id)
    {
        var tickets = await ef.Tickets.Where(x => x.UserId == id)
            .OrderByDescending(x=> x.CreatedAt)
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
#pragma warning restore CS8604 // Possible null reference argument.
}