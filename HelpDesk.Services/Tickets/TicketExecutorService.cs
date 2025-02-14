using HelpDesk.Models.DLA.Tickets;
using HelpDesk.Models.Dto.Tickets.Executor;
using HelpDesk.Models.Dto.Tickets.History;
using HelpDesk.Models.PLA.Accounts;
using HelpDesk.Models.PLA.Tickets;
using HelpDesk.Services.Identity;
using HelpDesk.Storage;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services.Tickets;

public class TicketExecutorService(
    HelpDeskContext ef,
    TicketHistoryService ticketHistoryService,
    IdentityService identityService,
    IMapper mapper)
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
        var executor = mapper.Map<TicketExecutor>(assignExecutorTicket);
        await ef.AddAsync(executor);
        await ef.SaveChangesAsync();
        var identity = await identityService.GetAccountById(executor.UserId);
        if (identity is null) throw new NullReferenceException("Исполнитель не найден");
        await ticketHistoryService.CreateTicketHistory(
            new TicketHistoryNew(assignExecutorTicket.TicketId,
                assignExecutorTicket.AppointedWhoId,
                $"Назначил(а) исполнителя по заявке: {identity.LastName} {identity.LastName}", false));
    }

    public async Task RemoveExecutor(long idExecutor, long currentAccount)
    {
        var executor = await ef.TicketExecutors.FirstOrDefaultAsync(x => x.Id == idExecutor);
        if (executor is null) throw new Exception("Исполнитель не найден");
        ef.Remove(executor);
        await ef.SaveChangesAsync();
        var identity = await identityService.GetAccountById(executor.UserId);
        if (identity is null) throw new NullReferenceException("Исполнитель не найден");
        await ticketHistoryService.CreateTicketHistory(
            new TicketHistoryNew(executor.TicketId,
                currentAccount,
                $"Снял(а) исполнителя по заявке: {identity.LastName} {identity.LastName}", false));
    }
}