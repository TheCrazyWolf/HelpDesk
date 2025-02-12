using HelpDesk.Models.DLA.Identity;
using HelpDesk.Models.Enums.Identity;
using HelpDesk.Models.PLA.Common;

namespace HelpDesk.Models.PLA.Accounts;

public class IdentityView : PlaEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public IdentityType IdentityType { get; set; }

    public IdentityView()
    {
        
    }

    public IdentityView(Account? xUser)
    {
        if (xUser != null)
        {
            FirstName = xUser.FirstName;
            LastName = xUser.LastName;
            MiddleName = xUser.MiddleName;
        }
    }
}