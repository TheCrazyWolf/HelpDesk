using HelpDesk.Models.DLA.Tickets;
using HelpDesk.Models.Dto.Tickets;
using HelpDesk.Models.Dto.Tickets.Tickets;
using HelpDesk.Services.Documents;
using HelpDesk.Storage;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services.Tickets;

public class TicketService(HelpDeskContext ef, DocumentService documentService, IMapper mapper)
{
    public async Task<long> CreateTicket(TicketCreateDto ticket)
    {
        var ticketEntity = mapper.Map<Ticket>(ticket);
        await ef.AddAsync(ticketEntity);
        await ef.SaveChangesAsync();
        await documentService.AttachDocumentToTicket(ticketEntity, ticket.DocumentsIds);
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
}