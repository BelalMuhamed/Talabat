using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.Identity
{
    public class UserApplication:IdentityUser
    {
        public string DisplayName { get; set; }
        public virtual Address address { get; set; }

    }
}
