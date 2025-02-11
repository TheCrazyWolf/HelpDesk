using HelpDesk.Models.Dto.Common;
using HelpDesk.Models.Enums.Identity;

namespace HelpDesk.Models.Dto.Auth;

public class DeskToken : WithId
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public IdentityType Role { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime ExpiresAt { get; set; }

    public bool IsValid()
    {
        return ExpiresAt <= DateTime.Now;
    }

}