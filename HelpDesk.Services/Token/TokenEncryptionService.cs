using Aes.Encryptor.Interfaces;
using HelpDesk.Models.Dto.Auth;
using Newtonsoft.Json;

namespace HelpDesk.Services.Token;

public class TokenEncryptionService(string encryptionKey, IAesService aesService)
{
    public string EncryptToken(DeskToken token)
    {
        return aesService.Encrypt(JsonConvert.SerializeObject(token), encryptionKey);
    }

    public DeskToken? DecryptToken(string? token)
    {
        try
        {
            return JsonConvert.DeserializeObject<DeskToken>(aesService.Decrypt(token, encryptionKey));
        }
        catch
        {
            return null;
        }
    }
}