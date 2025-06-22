using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Product.Core.Entities;
using System.Security.Claims;

namespace Product.API.Extensions
{
    public static class UserManagerExtendsions
    {
        public static async Task<AppUser> FidndUserByClaimPrincipalWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var email = user?.Claims.FirstOrDefault(x => x.Type==ClaimTypes.Email)?.Value;
            return await userManager.Users.Include(x => x.Address).SingleOrDefaultAsync();
        }

        public static async Task<AppUser> FidndEmailByClaimPrincipal(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var email = user?.Claims.FirstOrDefault(x => x.Type==ClaimTypes.Email)?.Value;
            return await userManager.Users.SingleOrDefaultAsync();
        }
    }
}
