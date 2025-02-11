using HelpDesk.Models.DLA.Tickets;
using HelpDesk.Models.PLA.Documents;
using HelpDesk.Storage;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services.Documents;

public class DocumentService(HelpDeskContext ef, IMapper mapper)
{
    public async Task<IList<DeskDocumentView>> GetDocumentByTicket(long idTicket)
    {
        return mapper.Map<IList<DeskDocumentView>>(await ef.Documents.Where(x => x.TicketId == idTicket).ToListAsync());
    }
    
    public async Task<IList<DeskDocumentView>> GetDocumentByHistoryTicket(long idHistoryTicket)
    {
        return mapper.Map<IList<DeskDocumentView>>(await ef.Documents.Where(x => x.TicketHistoryId == idHistoryTicket).ToListAsync());
    }
    
    
    public async Task DeleteDocumentId(long idDocument)
    {
        var document = await ef.Documents.FirstOrDefaultAsync(x=> x.Id == idDocument);
        if(document is null) throw new Exception("Документ не найден");
        if(document.TicketHistoryId is not null || document.TicketId is not null) throw new Exception("Этот документ используется в системе");
        ef.Remove(document);
        await ef.SaveChangesAsync();
    }

    public async Task AttachDocumentToTicket(Ticket ticketEntity, IList<long> ticketDocumentsIds)
    {
        await AttachDocumentToTicket(ticketEntity.Id, ticketDocumentsIds);
    }
    
    public async Task AttachDocumentToTicket(long ticketEntityId, IList<long> ticketDocumentsIds)
    {
        foreach (var ticketDocumentId in ticketDocumentsIds)
        {
            var document = await ef.Documents.FirstOrDefaultAsync(x=> x.Id == ticketDocumentId);
            if(document is null) continue;
            document.TicketId = ticketEntityId;
            ef.Update(document);
        }
        await ef.SaveChangesAsync();
    }
}