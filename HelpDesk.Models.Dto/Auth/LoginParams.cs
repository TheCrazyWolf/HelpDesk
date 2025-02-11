using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models.Dto.Auth;

public class LoginParams
{
    [StringLength(35, MinimumLength = 3)] public string Username { get; set; } = string.Empty;
    [StringLength(35, MinimumLength = 3)] public string Password { get; set; } = string.Empty;
    public bool IsRememberMe { get; set; }
}