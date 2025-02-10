using HelpDesk.Models.Dto.Auth;
using HelpDesk.Services.Utils;
using HelpDesk.Storage;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services.Token;

public class AuthService(HelpDeskContext ef, IMapper mapper)
{
    public async Task<DeskToken?> AuthenticateUser(LoginParams loginParams)
    {
        var user = await ef.Accounts.FirstOrDefaultAsync(x=> x.Password == loginParams.Password.GetHash() 
                                                             && x.Login == loginParams.Username);

        if (user is not null)
        {
            var dto = mapper.Map<DeskToken>(user);
            dto.StartAt = DateTime.Now;
            dto.ExpiresAt = loginParams.IsRememberMe ? dto.StartAt.AddYears(1) : dto.StartAt.AddDays(1);
            return dto;
        }
        
        // запрос к MFC
        return null;
    }
}