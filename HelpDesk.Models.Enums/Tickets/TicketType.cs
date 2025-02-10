using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models.Enums.Tickets;

public enum TicketType
{
    [Display(Name = "Неисправность")]
    Malfunction,
    [Display(Name = "Закупка")]
    Purchase,
}