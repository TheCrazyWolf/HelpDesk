using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models.Enums.Identity;

public enum IdentityType
{
    [Display(Name = "Студент")]
    Student,
    [Display(Name = "Преподаватель")]
    Teacher,
    [Display(Name = "ИТ отдел")]
    TechSupport
}