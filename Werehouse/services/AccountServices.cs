using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Warehouse.Data;
using Warehouse.Data;
using Warehouse.Models;
namespace Warehouse.services
{
    public class AccountServices: IAccountService
    {

        SignInManager<AppUser> signInManager;
        UserManager<AppUser> userManager;
        WarehouseContext warehouseContext;
        public AccountServices( SignInManager<AppUser> _signInManager, UserManager<AppUser> _userManager, WarehouseContext _warehouseContext)
        {
        
            signInManager = _signInManager;
            userManager = _userManager;
            warehouseContext = _warehouseContext;
        }

        public async Task<bool> LogInProccess(UserLogin userLogin)
        {
            
            if (userLogin.UserName != null && userLogin.Password != null )
            {
                AppUser user = await userManager.FindByNameAsync(userLogin.UserName);
                bool IsActive = user.IsActive;
                bool passwordValid = await userManager.CheckPasswordAsync(user, userLogin.Password);
                if (user != null && passwordValid && IsActive)
                {
                    var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

                    var groups = await warehouseContext.UserGroup
                        .Where(ug => ug.UserId == user.Id)
                        .Select(ug => ug.Group.Name)
                        .ToListAsync();

                    foreach (var group in groups)
                    {
                        identity.AddClaim(new Claim("Group", group));
                    }
                    await signInManager.SignInWithClaimsAsync(user, isPersistent: false, identity.Claims);
                    return true;

                }
            }
            return false;

        }


        public async Task LogOutProccess()
        {
            await signInManager.SignOutAsync();
        }
    }
}
