using HelpDesk.Models.DLA.Identity;
using HelpDesk.Models.DLA.Tickets;
using HelpDesk.Models.Dto.Auth;
using HelpDesk.Models.Dto.Tickets.Executor;
using HelpDesk.Models.Dto.Tickets.History;
using HelpDesk.Models.PLA.Accounts;
using HelpDesk.Models.PLA.Tickets;
using HelpDesk.Services.Identity;
using HelpDesk.Services.ThrowHelpers;
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

    public async Task AssignNewExecutor(AssignExecutorTicket assignExecutorTicket, DeskToken? deskToken)
    {
        var executor = mapper.Map<TicketExecutor>(assignExecutorTicket);
        executor.AppointedWhoId = deskToken.Id;
        await ef.AddAsync(executor);
        await ef.SaveChangesAsync();
        var identity = await identityService.GetAccountById(executor.UserId);
        identity.ThrowIfNull(nameof(Account));
        await ticketHistoryService.NewHistoryAssignedOfTicket(assignExecutorTicket, identity, deskToken);
    }

    public async Task RemoveExecutor(long idExecutor, DeskToken? deskToken)
    {
        var executor = await ef.TicketExecutors.FirstOrDefaultAsync(x => x.Id == idExecutor);
        executor.ThrowIfNull(nameof(TicketExecutor));
        ef.Remove(executor);
        await ef.SaveChangesAsync();
        var identity = await identityService.GetAccountById(executor.UserId);
        identity.ThrowIfNull(nameof(Account));
        await ticketHistoryService.NewHistoryUnAssignedOfTicket(executor, identity, deskToken);
    }
}