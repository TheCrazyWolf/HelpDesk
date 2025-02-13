using HelpDesk.Models.DLA.Documents;
using HelpDesk.Models.DLA.Tickets;
using HelpDesk.Models.PLA.Documents;
using HelpDesk.Storage;
using MapsterMapper;
using Microsoft.AspNetCore.Components.Forms;
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
            //ef.Update(document);
        }
        await ef.SaveChangesAsync();
    }

    public async Task AttachDocumentToTicketHistory(TicketHistory ticketHistoryEntity, IList<long> ticketHistoryDocumentsIds)
    {
        await AttachDocumentToTicketHistory(ticketHistoryEntity.Id, ticketHistoryDocumentsIds);
    }
    
    public async Task AttachDocumentToTicketHistory(long idTicketHistoryEntity, IList<long> ticketHistoryDocumentsIds)
    {
        foreach (var ticketDocumentId in ticketHistoryDocumentsIds)
        {
            var document = await ef.Documents.FirstOrDefaultAsync(x=> x.Id == ticketDocumentId);
            if(document is null) continue;
            document.TicketHistoryId = idTicketHistoryEntity;
            ef.Update(document);
        }
        await ef.SaveChangesAsync();
    }

    public async Task<DeskDocumentView> UploadFile(IBrowserFile file, long ownerFileId)
    {
        var userId = ownerFileId;
        var filesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "files");
        if (!Directory.Exists(filesDirectory)) Directory.CreateDirectory(filesDirectory);
        var userDirectory = Path.Combine(filesDirectory, userId.ToString());
        if (!Directory.Exists(userDirectory)) Directory.CreateDirectory(userDirectory);
        var filePath = Path.Combine(userDirectory, file.Name);
        await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        await file.OpenReadStream().CopyToAsync(fileStream);
        var entityFile = new DeskDocument { UserId = userId, Path = filePath, FileName = file.Name, UploadedAt = DateTime.Now };
        await AddFileAsync(entityFile);
        return mapper.Map<DeskDocumentView>(entityFile);
    }

    public async Task<DeskDocument?> GetDocumentById(long id)
    {
        return await ef.Documents.FirstOrDefaultAsync(x=> x.Id == id);
    }
    public async Task<MemoryStream> DownloadFileAsync(long fileId)
    {
        var file = await GetDocumentById(fileId);
        if (file is null) throw new Exception("Файл не найден");
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), file.Path);
        var memory = new MemoryStream();
        await using var stream = new FileStream(fullPath, FileMode.Open);
        await stream.CopyToAsync(memory);
        return memory;
    }

    private async Task AddFileAsync(DeskDocument entityFile)
    {
        await ef.AddAsync(entityFile);
        await ef.SaveChangesAsync();
    }

    public async Task RemoveFile(long fileId)
    {
        var file = await GetDocumentById(fileId);
        if (file is null) throw new Exception("Файл не найден");
        if(await IsUsedDocument(file.Id)) throw new Exception("Документ используется в документах ");
        ef.Remove(file);
        await ef.SaveChangesAsync();
    }

    public async Task<bool> IsUsedDocument(long documentId)
    {
        return await ef.Documents.AnyAsync(x=> x.Id == documentId && (x.TicketHistoryId != null || x.TicketId != null));
    }
}