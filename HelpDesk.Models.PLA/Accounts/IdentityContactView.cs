using HelpDesk.Models.PLA.Common;

namespace HelpDesk.Models.PLA.Accounts;

public class IdentityContactView : PlaEntity
{
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}