namespace HelpDesk.Models.Dto.Identity;

public class IdentityChangePassword
{
    public long Id { get; set; }
    public string Password { get; set; } = string.Empty;
}