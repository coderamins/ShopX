using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ShopX.Catalog.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;

namespace Shopx.Catalog.IntegrationTests
{
    public class CatalogApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer;
        public CatalogApiFactory()
        {
            _dbContainer = new PostgreSqlBuilder()
              .WithDatabase("shopxdb")
              .WithUsername("postgres")
              .WithPassword("Admin123")
              .WithImage("postgres:16-alpine")
              .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // حذف DbContext  اصلی
                services.RemoveAll(typeof(DbContextOptions<CatalogDbContext>));

                // اضافه کردن DbContext روی دیتابیس تست
                services.AddDbContext<CatalogDbContext>(options =>
                    options.UseNpgsql(GetConnectionString()));
            });
        }

        public string GetConnectionString()
                => "Host=localhost;Port=5432;Database=shopxdb;Username=postgres;Password=Admin123";

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            using var scope = Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
            db.Database.Migrate();
        }

        public async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}
