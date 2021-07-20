using Microsoft.AspNetCore.Identity;

namespace SignUp.Core.Models
{
    public class Role : IdentityRole<int>
    {
        public string Description { get; set; }
    }
}
