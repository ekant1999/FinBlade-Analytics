using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace api.Modles
{
    public class AppUser : IdentityUser
    {
        public List<Portfolio> Portfolios {get; set;} = new List<Portfolio>();
    }
}