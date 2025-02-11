using HelpDesk.Models.Dto.Tickets.Executor;
using HelpDesk.Models.PLA.Accounts;
using HelpDesk.Models.PLA.Tickets;
using HelpDesk.Storage;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services.Tickets;

public class TicketExecutorService(HelpDeskContext ef, IMapper mapper)
{

    public async Task<IList<TicketExecutorView>> GetExecutorsByTicket(long idTicket)
    {
        var executors = await ef.TicketExecutors.Include(x => x.User)
            .Where(x => x.TicketId == idTicket)
            .Select(x => new TicketExecutorView
            {
                Id = x.Id,
                Identity = new IdentityView(x.User)
            })
            .ToListAsync();
        
        return executors;
    }
    
    public async Task AssignNewExecutor(AssignExecutorTicket assignExecutorTicket)
    {
        var executor = mapper.Map<AssignExecutorTicket>(assignExecutorTicket);
        await ef.AddAsync(executor);
        await ef.SaveChangesAsync();
        // запись в TicketHistory
    }
    
    public async Task RemoveExecutor(long idExecutor)
    {
        var executor = await ef.TicketHistories.FirstOrDefaultAsync(x=> x.Id == idExecutor);
        if (executor is null) throw new Exception("Исполнитель не найден");
        ef.Remove(executor);
        await ef.SaveChangesAsync();
        // запись в TicketHistory
    }
}