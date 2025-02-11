using HelpDesk.Models.Dto.Identity;
using HelpDesk.Models.Enums.Identity;
using HelpDesk.Models.PLA.Accounts;
using HelpDesk.Services.Utils;
using HelpDesk.Storage;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services.Identity;

public class IdentityService(HelpDeskContext ef, IMapper mapper)
{
    public async Task<IList<IdentityView>> GetIdentityViews(IdentityType identityType)
    {
        return mapper.Map<IList<IdentityView>>(await ef.Accounts.Where(x => x.IdentityType == identityType)
            .ToListAsync());
    }
    
    public async Task<IdentityView?> GetIdentityView(long id)
    {
        var identity = await ef.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        return identity is not null ? mapper.Map<IdentityView>(identity) : null;
    }

    public async Task UpdateContactData(IdentityUpdateContact contactData)
    {
        var user = await ef.Accounts.FirstOrDefaultAsync(x => x.Id == contactData.Id);
        if (user is null) throw new NullReferenceException("Пользователь не найден");
        mapper.Map(contactData, user);
        await ef.SaveChangesAsync();
    }
    
    public async Task ChangeRole(IdentityChaneRole changeRole)
    {
        var user = await ef.Accounts.FirstOrDefaultAsync(x => x.Id == changeRole.Id);
        if (user is null) throw new NullReferenceException("Пользователь не найден");
        mapper.Map(changeRole, user);
        await ef.SaveChangesAsync();
    }
    
    public async Task ChangeLogin(IdentityChangeLogin changeLogin)
    {
        var user = await ef.Accounts.FirstOrDefaultAsync(x => x.Id == changeLogin.Id);
        if (user is null) throw new NullReferenceException("Пользователь не найден");
        if(user.IsMfcIntegration) throw new NullReferenceException("Эта учётная связана с mfc.samgk.ru. Менять логины или пароль запрещено.");
        var newLogin = await ef.Accounts.FirstOrDefaultAsync(x => x.Login == changeLogin.UserName);
        if (newLogin is not null) throw new NullReferenceException("Этот логин уже используется");
        mapper.Map(changeLogin, user);
        await ef.SaveChangesAsync();
    }

    public async Task ChangePassword(IdentityChangePassword changePassword)
    {
        var user = await ef.Accounts.FirstOrDefaultAsync(x => x.Id == changePassword.Id);
        if (user is null) throw new NullReferenceException("Пользователь не найден");
        if(user.IsMfcIntegration) throw new NullReferenceException("Эта учётная связана с mfc.samgk.ru. Менять логины или пароль запрещено.");
        changePassword.Password = changePassword.Password.GetHash();
        mapper.Map(changePassword, user);
        await ef.SaveChangesAsync();
    }

    public async Task RemoveIdentity(long userId)
    {
        var user = await ef.Accounts.FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null) return;
        ef.Remove(user);
        await ef.SaveChangesAsync();
    }

    public async Task RemoveIdentity(IList<long> userIds)
    {
        var users = await ef.Accounts.Where(x => userIds.Contains(x.Id)).ToListAsync();
        foreach (var account in users) ef.Remove(account);
        await ef.SaveChangesAsync();
    }
}