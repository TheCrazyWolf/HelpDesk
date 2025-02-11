using HelpDesk.Models.Enums.Identity;
using HelpDesk.Models.PLA.Common;

namespace HelpDesk.Models.PLA.Account;

public class IdentityView : PlaEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public IdentityType IdentityType { get; set; }
}