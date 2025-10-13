using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ShopX.Identity.Infrastructure.Data
{
    public class DesignTimeIdentityDbContextFactory
          : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext CreateDbContext(string[] args)
        {
            // مسیر فایل appsettings.json در پروژه‌ی API
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../ShopX.Identity.API");

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = config.GetConnectionString("IdentityDb");

            var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new IdentityDbContext(optionsBuilder.Options);
        }
    }
}
