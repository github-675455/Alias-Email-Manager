using Microsoft.AspNetCore.Identity;
using System;

namespace AliasManager.Model
{
    public class User : IdentityUser
    {
        public String Username;
        public String Password;
        public String Salt;
    }
}
