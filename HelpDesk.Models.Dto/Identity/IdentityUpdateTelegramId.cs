using HelpDesk.Models.Dto.Common;

namespace HelpDesk.Models.Dto.Identity;

public class IdentityUpdateTelegramId : WithId
{
    public long? TelegramId { get; set; }
}