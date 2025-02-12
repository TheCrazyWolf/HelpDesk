using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models.Enums.Tickets;

public enum TicketStatus
{
    [Display(Name = "Новая")]
    Created,
    [Display(Name = "В работе")]
    InProgress,
    [Display(Name = "Заморожена")]
    OnHold,
    [Display(Name = "Требуется обратная связь")]
    AwaitFeedback,
    [Display(Name = "Ожидание")]
    Pending,
    [Display(Name = "Решена")]
    Resolved,
    [Display(Name = "Закрыта")]
    Closed,
    
}