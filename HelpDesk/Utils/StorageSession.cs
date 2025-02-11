using Blazored.LocalStorage;
using HelpDesk.Models.Dto.Auth;
using HelpDesk.Services.Token;

namespace HelpDesk.Utils;

public class StorageSession(TokenEncryptionService tokenEncryptionService, 
    ILocalStorageService localStorageService, AuthService authService)
{
    private readonly string _paramStorageNameToken = "Token";
    
    private DeskToken? _deskToken;
    
    public async Task<DeskToken?> GetCurrentAccessToken()
    {
        var encrypted = await localStorageService.GetItemAsync<string>(_paramStorageNameToken);
        _deskToken = tokenEncryptionService.DecryptToken(encrypted);
        return _deskToken?.ExpiresAt < DateTime.Now ? null : _deskToken!;
    }

    public async Task SetAccessToken(DeskToken deskToken)
    {
        var encrypted = tokenEncryptionService.EncryptToken(deskToken);
        await localStorageService.SetItemAsync(_paramStorageNameToken, encrypted);
        _deskToken = deskToken;
    }

    public async Task Login(LoginParams loginParams)
    {
        var deskToken = await authService.AuthenticateUser(loginParams);
        if(deskToken is null) throw new Exception("Failed to authenticate user.");
        await SetAccessToken(deskToken);
    }

    public async void ClearSession()
    {
        _deskToken = null!;
        await localStorageService.SetItemAsync(_paramStorageNameToken, string.Empty);
    }
}