using HelpDesk.Models.Dto.Auth;

namespace HelpDesk.Services.Token;

public class TokenEncryptionService(string encryptionKey)
{
    public async Task<string> EncryptToken(DeskToken token)
    {
        return string.Empty;
    }

    public async Task<DeskToken> DecryptToken(string token)
    {
        return new DeskToken();
    }

    public bool IsValid(DeskToken token)
    {
        return token.ExpiresAt <= DateTime.Now;
    }
    
    public bool IsValid(string token)
    {
        return IsValid(DecryptToken(token).GetAwaiter().GetResult());
    }
}