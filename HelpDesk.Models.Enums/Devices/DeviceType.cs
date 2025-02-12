using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models.Enums.Devices;

public enum DeviceType
{
    [Display(Name = "Компьютер/Ноутбук")] Computer,
    [Display(Name = "Принтер/Сканер/Мфу")] Printer,
    [Display(Name = "Сетевое оборудование")] Network,
    [Display(Name = "Мобильное устройство/Планшет")] Mobile,
    [Display(Name = "Проектор/Интерактивная доска")] Projector,
    [Display(Name = "Серверное оборудование")] Server,
    [Display(Name = "Другое")] Other
}