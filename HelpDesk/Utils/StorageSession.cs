using Blazored.LocalStorage;
using HelpDesk.Models.Dto.Auth;
using HelpDesk.Services.Token;

namespace HelpDesk.Utils;

public class StorageSession(TokenEncryptionService tokenEncryptionService, 
    ILocalStorageService localStorageService, AuthService authService)
{
    private readonly string _paramStorageNameToken = "Token";
    
    public DeskToken? DeskToken { get; private set; }
    
    public async Task<DeskToken?> GetCurrentAccessToken()
    {
        var encrypted = await localStorageService.GetItemAsync<string>(_paramStorageNameToken);
        DeskToken = tokenEncryptionService.DecryptToken(encrypted);
        return DeskToken?.ExpiresAt < DateTime.Now ? null : DeskToken!;
    }

    public async Task SetAccessToken(DeskToken deskToken)
    {
        var encrypted = tokenEncryptionService.EncryptToken(deskToken);
        await localStorageService.SetItemAsync(_paramStorageNameToken, encrypted);
        DeskToken = deskToken;
    }

    public async Task Login(LoginParams loginParams)
    {
        var deskToken = await authService.AuthenticateUser(loginParams);
        if(deskToken is null) throw new Exception("Failed to authenticate user.");
        await SetAccessToken(deskToken);
    }

    public async void ClearSession()
    {
        DeskToken = null!;
        await localStorageService.SetItemAsync(_paramStorageNameToken, string.Empty);
    }
}