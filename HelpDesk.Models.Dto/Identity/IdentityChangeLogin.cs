using HelpDesk.Models.Dto.Common;

namespace HelpDesk.Models.Dto.Identity;

public class IdentityChangeLogin : WithId
{
    public string UserName { get; set; } = string.Empty;
}