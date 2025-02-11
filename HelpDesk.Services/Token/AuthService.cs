using HelpDesk.Mfc.Authorization;
using HelpDesk.Mfc.Authorization.Models;
using HelpDesk.Models.DLA.Identity;
using HelpDesk.Models.Dto.Auth;
using HelpDesk.Models.Enums.Identity;
using HelpDesk.Services.Utils;
using HelpDesk.Storage;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services.Token;

public class AuthService(HelpDeskContext ef, MfcServiceLogon mfcServiceLogon, IMapper mapper)
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
        
        var result = await mfcServiceLogon.Login(loginParams);
        if(result is null || result.Code is "404") throw new Exception("Неверный логин или пароль.");
        
        return await CreateAndGetToken(loginParams, result);
    }

    public async Task<DeskToken> CreateAndGetToken(LoginParams loginParams, AuthMfcResult mfcResult)
    {
        var user = new Account
        {
            Login = loginParams.Username,
            Password = loginParams.Password.GetHash(),
            FirstName = mfcResult.Name,
            LastName = mfcResult.Surname,
            MiddleName = mfcResult.Name,
            IdentityType = mfcResult.IsTeacher ? IdentityType.Teacher : IdentityType.Student,
        };
        
        await ef.AddAsync(user);
        await ef.SaveChangesAsync();
        var dto = mapper.Map<DeskToken>(user);
        dto.StartAt = DateTime.Now;
        dto.ExpiresAt = loginParams.IsRememberMe ? dto.StartAt.AddYears(1) : dto.StartAt.AddDays(1);
        return dto;
    }
}