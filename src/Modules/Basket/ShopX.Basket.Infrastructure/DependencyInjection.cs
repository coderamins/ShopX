using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ShopX.Basket.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
                           cfg.RegisterServicesFromAssembly(Assembly.Load("ShopX.Basket.Application")));

            return services;

        }
    }
}
