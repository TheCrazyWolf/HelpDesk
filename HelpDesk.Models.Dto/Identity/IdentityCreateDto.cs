using HelpDesk.Models.Enums.Identity;

namespace HelpDesk.Models.Dto.Identity;

public class IdentityCreateDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public IdentityEnum IdentityType { get; set; }
}