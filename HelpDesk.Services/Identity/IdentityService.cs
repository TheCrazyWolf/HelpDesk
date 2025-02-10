using HelpDesk.Models.Dto.Auth;
using HelpDesk.Models.Dto.Identity;
using HelpDesk.Models.Enums.Identity;
using HelpDesk.Models.PLA.Account;
using HelpDesk.Storage;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services.Identity;

public class IdentityService(HelpDeskContext ef, IMapper mapper)
{
    public async Task<IList<IdentityView>> GetIdentityViews(IdentityEnum identityEnum)
    {
        return mapper.Map<IList<IdentityView>>(await ef.Accounts.Where(x => x.IdentityType == identityEnum)
            .ToListAsync());
    }

    public async Task ChangeRole(IdentityChaneRole changeRole)
    {
        var user = await ef.Accounts.FirstOrDefaultAsync(x=> x.Id == changeRole.Id);
        if(user is null) throw new NullReferenceException("User does not exist");
        mapper.Map(changeRole, user);
        await ef.SaveChangesAsync();
    }


    public async Task ChangeLogin(IdentityChangeLogin changeLogin)
    {
        var user = await ef.Accounts.FirstOrDefaultAsync(x=> x.Id == changeLogin.Id);
        if(user is null) throw new NullReferenceException("User does not exist");
        var newLogin = await ef.Accounts.FirstOrDefaultAsync(x=> x.Login == changeLogin.UserName);
        if(newLogin is not null) throw new NullReferenceException("This is login is already in use");
        mapper.Map(changeLogin, user);
        await ef.SaveChangesAsync();
    }

    public async Task ChangePassword(IdentityChangePassword changePassword)
    {
        var user = await ef.Accounts.FirstOrDefaultAsync(x=> x.Id == changePassword.Id);
        if(user is null) throw new NullReferenceException("User does not exist");
        // convert pass to sha
        mapper.Map(changePassword, user);
        await ef.SaveChangesAsync();
    }

    public async Task RemoveIdentity(long userId)
    {
        var user = await ef.Accounts.FirstOrDefaultAsync(x=> x.Id == userId);
        if(user is null) return;
        ef.Remove(user);
        await ef.SaveChangesAsync();
    }

    public async Task RemoveIdentity(IList<long> userIds)
    {
        var users = await ef.Accounts.Where(x=> userIds.Contains(x.Id)).ToListAsync();
        foreach (var account in users) ef.Remove(account);
        await ef.SaveChangesAsync();
    }
}