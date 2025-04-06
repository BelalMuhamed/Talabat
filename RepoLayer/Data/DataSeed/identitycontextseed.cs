using CoreLayer.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Data.DataSeed
{
   public static class identitycontextseed 
    
    {
     

      
        public async static Task IdentitySeeding(UserManager<UserApplication> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new UserApplication()
                {
                    DisplayName="Belal Basal",
                   Email="basalbelal25@gmail.com",
                   UserName="BelalBasal",
                   PhoneNumber="01008319684"

                };
                await  userManager.CreateAsync(user,"P@ssword123");
            }
        }
    }
}
