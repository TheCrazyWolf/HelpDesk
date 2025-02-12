using HelpDesk.Models.DLA.Common;
using HelpDesk.Models.Enums.Identity;

namespace HelpDesk.Models.DLA.Identity;

public class Account : DlaEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public long? TelegramId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public IdentityType IdentityType { get; set; }
    public bool IsMfcIntegration { get; set; }
}