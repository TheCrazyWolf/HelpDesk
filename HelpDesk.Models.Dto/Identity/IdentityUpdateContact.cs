using HelpDesk.Models.Dto.Common;

namespace HelpDesk.Models.Dto.Identity;

public class IdentityUpdateContact : WithId
{
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}