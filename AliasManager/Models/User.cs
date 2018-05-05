using Microsoft.AspNetCore.Identity;
using System;

namespace AliasManager.Models
{
    public class User : IdentityUser
    {
        public String Username;
        public String Password;
        public String Salt;
    }
}
