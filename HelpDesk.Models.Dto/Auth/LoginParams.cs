namespace HelpDesk.Models.Dto.Auth;

public class LoginParams
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsRememberMe { get; set; }
}