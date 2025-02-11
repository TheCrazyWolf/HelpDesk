using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models.Enums.Identity;

public enum IdentityType
{
    [Display(Name = "Студент")]
    Student,
    [Display(Name = "Сотрудник")]
    Employee,
    [Display(Name = "ИТ отдел")]
    TechSupport
}