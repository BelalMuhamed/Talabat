using CoreLayer.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace APIsLayer.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<UserApplication> GetAddress(this UserManager<UserApplication> usermanager, ClaimsPrincipal User)
        {
            var email =User.FindFirstValue(ClaimTypes.Email);
            var user= await usermanager.Users.Include(u => u.address).FirstOrDefaultAsync(u => u.Email == email);
            return user;

        }
        
    }
}
