using Microsoft.AspNetCore.Identity;
using System;

namespace SignUp.Core.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
    }
}
