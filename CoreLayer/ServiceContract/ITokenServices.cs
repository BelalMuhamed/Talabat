using CoreLayer.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.ServiceContract
{
   public interface ITokenServices
    {
        public  Task<string> CreateToken(UserApplication user,UserManager<UserApplication> _userManager);
    }
}
