using HelpDesk.Mfc.Authorization.Models;
using HelpDesk.Models.Dto.Auth;
using Newtonsoft.Json;
using RestSharp;

namespace HelpDesk.Mfc.Authorization;

public class MfcServiceLogon
{
    private RestClient _restClient = new RestClient("https://mfc.samgk.ru/");

    public async Task<AuthMfcResult?> Login(LoginParams loginParams)
    {
        try
        {
            var options = new RestRequest("api/auth-new", Method.Post);
            options.AddBody(loginParams);
            return JsonConvert.DeserializeObject<AuthMfcResult>((await _restClient.ExecuteAsync(options)).Content ?? string.Empty);
        }
        catch
        {
            return null;
        }
    }
}