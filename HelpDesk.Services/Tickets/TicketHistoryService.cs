using HelpDesk.Models.DLA.Tickets;
using HelpDesk.Models.Dto.Tickets.History;
using HelpDesk.Models.PLA.Accounts;
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
    
    public async Task CreateTicketHistory(TicketHistoryNew ticketHistory)
    {
        var ticketHistoryEntity = mapper.Map<TicketHistory>(ticketHistory);
        ticketHistoryEntity.CreatedAt = DateTime.Now;
        await ef.AddAsync(ticketHistoryEntity);
        await ef.SaveChangesAsync();
        await documentService.AttachDocumentToTicketHistory(ticketHistoryEntity, ticketHistory.Files.Select(x=> x.Id).ToArray());
    }
    
}