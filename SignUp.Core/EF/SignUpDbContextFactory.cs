using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SignUp.Core.EF
{
    public class SignUpDbContextFactory : IDesignTimeDbContextFactory<SignUpDbContext>
    {
        public SignUpDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("SignUpDb");

            var optionsBuilder = new DbContextOptionsBuilder<SignUpDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new SignUpDbContext(optionsBuilder.Options);
        }
    }
}
