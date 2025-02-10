using HelpDesk.Models.Dto.Common;

namespace HelpDesk.Models.Dto.Identity;

public class IdentityChangePassword : WithId
{
    public string Password { get; set; } = string.Empty;
}