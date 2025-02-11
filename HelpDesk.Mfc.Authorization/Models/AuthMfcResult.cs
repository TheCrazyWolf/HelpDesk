namespace HelpDesk.Mfc.Authorization.Models;

public class AuthMfcResult
{
    public string Code { get; set; } = string.Empty;
    public int Id { get; set; }
    public string Group { get; set; }  = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Name { get; set; }  = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public bool IsTeacher { get; set; }
    public bool IsStudent { get; set; }
}