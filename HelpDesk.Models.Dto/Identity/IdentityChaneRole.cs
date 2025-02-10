using HelpDesk.Models.Enums.Identity;

namespace HelpDesk.Models.Dto.Identity;

public class IdentityChaneRole
{
    public long Id { get; set; }
    public IdentityEnum IdentityType { get; set; }
}