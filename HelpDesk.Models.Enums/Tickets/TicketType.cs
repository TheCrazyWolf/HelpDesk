using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models.Enums.Tickets;

public enum TicketType
{
    [Display(Name = "Мелкий инцидиент")] Incident,

    [Display(Name = "Установка программного обеспечения/Драйверов")]
    InstallSoftware,

    [Display(Name = "Ремонт/Другие неисправности")]
    Malfunction,

    [Display(Name = "Закупка оборудования/Списание")]
    Purchase,
    
    [Display(Name = "Другое")]
    Any,
}