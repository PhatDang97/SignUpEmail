using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignUp.Core.Models;

namespace SignUp.Core.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User> 
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(200);
            builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(200);
        }
    }
}