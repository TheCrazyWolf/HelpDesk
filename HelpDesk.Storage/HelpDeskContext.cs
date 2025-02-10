using HelpDesk.Models.DLA.Identity;
using HelpDesk.Models.DLA.Tickets;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Storage;

public class HelpDeskContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketExecutor> TicketExecutors { get; set; }
    public DbSet<TicketHistory> TicketHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=helpdesk.db");
        base.OnConfiguring(optionsBuilder);
    }
}