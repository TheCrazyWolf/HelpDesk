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
        return await ef.TicketHistories
            .Include(x=> x.User)
            .Where(x=> x.TicketId == idTicket)
            .Select(x=> new TicketHistoryView
            {
                Id = x.Id,
                CreatedBy = mapper.Map<IdentityView>(x.User!),
                CreatedAt = x.CreatedAt
            }).ToListAsync();
    }
    
    public async Task CreateTicketHistory(TicketHistoryNew ticketHistory)
    {
        var ticketHistoryEntity = mapper.Map<TicketHistory>(ticketHistory);
        await ef.AddAsync(ticketHistoryEntity);
        await ef.SaveChangesAsync();
        await documentService.AttachDocumentToTicketHistory(ticketHistoryEntity, ticketHistory.DocumentsIds);
    }
    
}