using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ShopX.Basket.Infrastructure.Persistence;

namespace ShopX.Basket.IntegrationTests
{
    public class BasketApiFactory:WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<BasketDbContext>));
                
                if(descriptor != null) 
                    services.Remove(descriptor);

                services.AddDbContext<BasketDbContext>(options =>
                    options.UseInMemoryDatabase("BasketTestDb"));
            });


        }
    }
}
