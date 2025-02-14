using Blazored.LocalStorage;
using HelpDesk.Models.Dto.Auth;
using HelpDesk.Services.Token;
using Microsoft.AspNetCore.Components;

namespace HelpDesk.Utils;

public class StorageSession(TokenEncryptionService tokenEncryptionService, NavigationManager navigationManager,
    ILocalStorageService localStorageService, AuthService authService)
{
    private readonly string _paramStorageNameToken = "Token";
    private string loginPage = "/auth/login";
    public DeskToken? DeskToken { get; private set; }
    
#pragma warning disable CS8603 // Possible null reference return.
    public async Task<DeskToken> GetCurrentAccessToken()
    {
        if (DeskToken != null && DeskToken.IsValid()) return DeskToken;
        var encrypted = await localStorageService.GetItemAsync<string>(_paramStorageNameToken);
        DeskToken = tokenEncryptionService.DecryptToken(encrypted);

        if (DeskToken == null || ! DeskToken.IsValid())
        {
            navigationManager.NavigateTo("/auth/login");
        }
        return DeskToken;
    }
#pragma warning restore CS8603 // Possible null reference return.

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