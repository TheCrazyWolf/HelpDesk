using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models.Enums.Tickets;

public enum TicketLevelPriority
{
    [Display(Name = "Низкий")] Low,
    [Display(Name = "Средний")] Medium,
    [Display(Name = "Высокий")] Hard
}