using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignUp.Core.Configurations;
using SignUp.Core.Models;
using System.Reflection;

namespace SignUp.Core.EF
{
    public class SignUpDbContext : IdentityDbContext<User, Role, int>
    {
        public SignUpDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());

            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaim");

            builder.Entity<IdentityUserRole<int>>().ToTable("UserRole").HasKey(x => new { x.UserId, x.RoleId });
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogin").HasKey(x => x.UserId);

            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserToken").HasKey(x => x.UserId);

            base.OnModelCreating(builder);
        }
    }
}
