using HelpDesk.Models.Dto.Auth;

namespace HelpDesk.Services.Token;

public class TokenEncryptionService
{
    private const string EncryptionKey = "Token";
    
    public async Task<string> EncryptToken(DeskToken token)
    {
        return string.Empty;
    }

    public async Task<DeskToken> DecryptToken(string token)
    {
        return new DeskToken();
    }

    public bool IsValideted(DeskToken token)
    {
        return token.ExpiresAt <= DateTime.Now;
    }
}